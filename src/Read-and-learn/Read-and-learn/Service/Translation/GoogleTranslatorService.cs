using Read_and_learn.Model.DataStructure;
using Read_and_learn.Service.Interface;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using File = System.IO.File;

namespace Read_and_learn.Service.Translation
{
    /// <summary>
    /// Implementation of <see cref="ITranslatorService"/> for <see cref="TranslationServicesProvider.Google"/>.
    /// </summary>
    public class GoogleTranslatorService : ITranslatorService
    {
        public async Task<WordTranslationResult> TranslateWord(string targetWord, string sourceLanguage)
        {
            WordTranslationResult result = (WordTranslationResult)await _Translate(targetWord, sourceLanguage);

            return result;
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

                string outputFile = Path.GetTempFileName();
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                    await wc.DownloadFileTaskAsync(url, outputFile);
                }

                // Get translated text
                if (File.Exists(outputFile))
                {
                    // Get phrase collection
                    string text = File.ReadAllText(outputFile);
                    int index = text.IndexOf(string.Format(",,\"{0}\"", sourceLanguage));

                    if (index == -1)
                    {
                        // Translation of single word
                        int startQuote = text.IndexOf('\"');
                        if (startQuote != -1)
                        {
                            int endQuote = text.IndexOf('\"', startQuote + 1);
                            if (endQuote != -1)
                            {
                                translation = text.Substring(startQuote + 1, endQuote - startQuote - 1);
                            }
                        }
                    }
                    else
                    {
                        // Translation of phrase
                        text = text.Substring(0, index);
                        text = text.Replace("],[", ",");
                        text = text.Replace("]", string.Empty);
                        text = text.Replace("[", string.Empty);
                        text = text.Replace("\",\"", "\"");

                        // Get translated phrases
                        string[] phrases = text.Split(new[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; (i < phrases.Count()); i += 2)
                        {
                            string translatedPhrase = phrases[i];
                            if (translatedPhrase.StartsWith(",,"))
                            {
                                i--;
                                continue;
                            }
                            translation += translatedPhrase + "  ";
                        }
                    }

                    // Fix up translation
                    translation = translation.Trim();
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
