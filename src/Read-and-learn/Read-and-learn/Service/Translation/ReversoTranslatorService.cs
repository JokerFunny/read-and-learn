using Read_and_learn.AppResources;
using Read_and_learn.Model.DataStructure;
using Read_and_learn.Model.Translation.ReversoApi;
using Read_and_learn.Model.Translation.ReversoApi.Text;
using Read_and_learn.Model.Translation.ReversoApi.Word;
using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Read_and_learn.Service.Translation
{
    /// <summary>
    /// Implementation of <see cref="ITranslatorService"/> for <see cref="TranslationServicesProvider.Reverso"/>.
    /// </summary>
    public class ReversoTranslatorService : ITranslatorService
    {
        private readonly ReversoApi.ReversoApi _api = new ReversoApi.ReversoApi();
        private static Regex _itemsRegex = new Regex(@"\w+|\s+|\W", RegexOptions.Compiled);
        private const int _chunkElementsCharMaxSize = 150;

        public async Task<TranslationResult> TranslatePart(string targetPart, string sourceLanguage)
        {
            string translationResult = null;

            try
            {
                const string url = "TranslateText";
                Language from = _ParseLanguage(sourceLanguage);
                Language to = _ParseLanguage();

                List<string> translationChunks = new List<string>();
                var regexMatches = _itemsRegex.Matches(targetPart);

                // split input text by chunks.
                if (regexMatches.Count > 0)
                {
                    var regexResult = regexMatches.Cast<Match>().Select(m => m.Value);
                    string translationChunk = string.Empty;

                    foreach (var element in regexResult)
                    {
                        if (translationChunk.Length + element.Length > _chunkElementsCharMaxSize)
                        {
                            translationChunks.Add(translationChunk);
                            translationChunk = string.Empty;
                        }

                        translationChunk += element;
                    }

                    translationChunks.Add(translationChunk);
                }

                // translate target text.
                foreach (var item in translationChunks)
                {
                    var translateTextRequest = new TranslateTextRequest(from, to)
                    {
                        Source = item
                    };

                    // translate target part.
                    var result = await _Translate<TranslateTextResponse>(url, translateTextRequest);

                    if (result.Success)
                        translationResult += result.Translation;
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
                Result = translationResult
            };
        }

        public async Task<WordTranslationResult> TranslateWord(string targetWord, string sourceLanguage)
        {
            var synonyms = new List<string>();
            var contexts = new List<string>();

            try
            {
                const string url = "TranslateWord";
                Language from = _ParseLanguage(sourceLanguage);
                Language to = _ParseLanguage();

                var translateWordRequest = new TranslateWordRequest(from, to)
                {
                    Word = targetWord,
                    Source = targetWord
                };

                var result = await _Translate<TranslatedResponse>(url, translateWordRequest);

                int contextCounter = 1;
                // Gets all variants of translation (limited by 5 values).
                foreach (var translations in result.Sources.FirstOrDefault()?.Translations.Take(5))
                {
                    synonyms.Add(translations.Translation);

                    // Gets all examples for this word translation (limited by 2 examples if exist).
                    foreach (var translationsContext in translations.Contexts.Take(2))
                        contexts.Add($"{contextCounter++}) {translationsContext.Source.Replace("<em>", "**").Replace("</em>", "**")} --- {translationsContext.Target.Replace("<em>", "**").Replace("</em>", "**")}");
                }
            }
            catch (Exception ex)
            {
                return new WordTranslationResult()
                {
                    Error = ex,
                    Result = ""
                };
            }

            return new WordTranslationResult()
            {
                Error = null,
                Result = synonyms.First(),
                Synonyms = synonyms,
                Contexts = contexts
            };
        }

        private Language _ParseLanguage(string targetLanguage = null)
        {
            var language = string.IsNullOrEmpty(targetLanguage) 
                ? UserSettings.Translation.SelectedLanguage
                : targetLanguage;

            if (Enum.TryParse(language, true, out Language supportedType))
                return supportedType;

            throw new Exception($"{AppResource.Translation_TargetLanguageNotSupported} {string.Join(", ", Enum.GetValues(typeof(Language)).Cast<Language>())}");
        }

        private async Task<T> _Translate<T>(string url, TranslateRequestBase request)
        {
            var result = await _api.SendPostRequest<T>(url, request);

            return result.Data;
        }
    }
}
