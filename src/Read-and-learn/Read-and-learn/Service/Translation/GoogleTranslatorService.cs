using Read_and_learn.Model.DataStructure;
using Read_and_learn.Service.Interface;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Read_and_learn.Service.Translation
{
    /// <summary>
    /// Implementation of <see cref="ITranslatorService"/> for <see cref="TranslationServicesProvider.Google"/>.
    /// </summary>
    public class GoogleTranslatorService : ITranslatorService
    {
        private const string _firstPartStartSymbols = "[[[\"";
        private const string _tranlationSeparator = "\",\"";
        private const string _innerPhraseStartIndex = "]\n,[\"";

        public async Task<WordTranslationResult> TranslateWord(string targetWord, string sourceLanguage)
        {
            var result = await _Translate(targetWord, sourceLanguage);

            return WordTranslationResult.GetWordTranslationResult(result);
        }

        public Task<TranslationResult> TranslatePart(string targetPart, string sourceLanguage)
            => _Translate(targetPart, sourceLanguage);

        private async Task<TranslationResult> _Translate(string targetText, string sourceLanguage)
        {
            string translation = string.Empty;
            string targetLanguage = UserSettings.Translation.SelectedLanguage;

            try
            {
                // Download translation
                string url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                                            sourceLanguage,
                                            targetLanguage,
                                            HttpUtility.UrlEncode(targetText));

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                var response = await request.GetResponseAsync().ConfigureAwait(false);
                HttpWebResponse httpResponse = (HttpWebResponse)response;

                string result = string.Empty;

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    using(Stream receiveStream = httpResponse.GetResponseStream())
                    using (StreamReader readStream = string.IsNullOrWhiteSpace(httpResponse.CharacterSet)
                        ? new StreamReader(receiveStream)
                        : new StreamReader(receiveStream, Encoding.GetEncoding(httpResponse.CharacterSet)))

                        result = await readStream.ReadToEndAsync();

                    httpResponse.Close();
                }

                // Get translated text
                if (!string.IsNullOrEmpty(result))
                {
                    // Get phrase collection
                    int index = result.IndexOf(string.Format(",\"{0}\"", sourceLanguage));

                    if (index == -1)
                    {
                        // Translation of single word
                        int startQuote = result.IndexOf('\"');
                        if (startQuote != -1)
                        {
                            int endQuote = result.IndexOf('\"', startQuote + 1);
                            if (endQuote != -1)
                            {
                                translation = result.Substring(startQuote + 1, endQuote - startQuote - 1);
                            }
                        }
                    }
                    else
                    {
                        // Translation of phrase
                        result = result.Substring(0, index);

                        int firstPartStartIndex = result.IndexOf(_firstPartStartSymbols);
                        translation += result.Substring(firstPartStartIndex + _firstPartStartSymbols.Length, 
                            result.IndexOf(_tranlationSeparator, firstPartStartIndex) - _tranlationSeparator.Length - 1);

                        result = result.Substring(result.IndexOf(_tranlationSeparator, firstPartStartIndex));

                        // check if translation contain additional phrases.
                        int phraseIndex = result.IndexOf(_innerPhraseStartIndex);
                        while (phraseIndex != -1)
                        {
                            result = result.Substring(phraseIndex + _innerPhraseStartIndex.Length);

                            translation += result.Substring(0, result.IndexOf(_tranlationSeparator));

                            phraseIndex = result.IndexOf(_innerPhraseStartIndex);
                        }
                    }

                    // Fix up translation
                    translation = translation.Trim();
                    translation = translation.Replace("  ", " ");
                    translation = translation.Replace("\\\"", "\"");
                    translation = translation.Replace(" \"", "\"");
                    translation = translation.Replace("\" ", "\"");
                    translation = translation.Replace(" ?", "?");
                    translation = translation.Replace(" !", "!");
                    translation = translation.Replace(" ,", ",");
                    translation = translation.Replace(" .", ".");
                    translation = translation.Replace(" ;", ";");
                }
            }
            catch (Exception ex)
            {
                return new TranslationResult()
                {
                    Error = ex,
                    Result = ""
                };
            }

            return new TranslationResult()
            {
                Error = null,
                Result = translation
            };
        }
    }
}
