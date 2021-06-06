using Read_and_learn.AppResources;
using Read_and_learn.Model.DataStructure;
using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YandexLinguistics.NET;
using YandexLinguistics.NET.Dictionary;
using YandexLinguistics.NET.Translator;

namespace Read_and_learn.Service.Translation
{
    /// <summary>
    /// Implementation of <see cref="ITranslatorService"/> for <see cref="TranslationServicesProvider.Yandex_Translator"/>.
    /// </summary>
    public class YandexTranslatorService : ITranslatorService
    {
        private TranslatorService _translatorService; 
        private DictionaryService _dictionaryService;
        private const string _blockedMessage = "A connection attempt failed because the connected party did not properly respond after " +
            "a period of time, or established connection failed because connected host has failed to respond. (dictionary.yandex.net:443)";

        /// <summary>
        /// Public ctor.
        /// </summary>
        public YandexTranslatorService()
        {
            _translatorService = new TranslatorService(AppSettings.Translation.YandexTranslationAPIKey);
            _dictionaryService = new DictionaryService(AppSettings.Translation.YandexDictionaryAPIKey);
        }

        public async Task<TranslationResult> TranslatePart(string targetPart, string sourceLanguage)
        {
            string translationResult;

            try
            {
                var targetLanguage = _ParseLanguage();

                // translate with auto-detect source language.
                var translation = await _translatorService.TranslateAsync(targetPart,
                       new LanguagePair(Language.None, targetLanguage), null, true);

                translationResult = translation.Texts[0];
            }
            catch(YandexLinguisticsException ex)
            {
                if (ex.Message == _blockedMessage)
                {
                    return new WordTranslationResult()
                    {
                        Result = AppResource.Translation_Yandex_BlockedInCountryError
                    };
                }
                else
                {
                    return new WordTranslationResult()
                    {
                        Error = ex,
                        Result = ""
                    };
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
            string translationResult = null;
            List<string> synonyms = null;

            try
            {
                var targetLanguage = _ParseLanguage();

                // translate with auto-detect source language.
                var translation = await _dictionaryService.LookupAsync(new LanguagePair(Language.None, targetLanguage), "time");
                
                var def0 = translation.Definitions[0];
                var tr = def0.Translations[0];
                translationResult = tr.Text;
                synonyms = (List<string>)tr.Synonyms.Select(s => s.Text);

            }
            catch (YandexLinguisticsException ex)
            {
                if (ex.Message == _blockedMessage)
                {
                    return new WordTranslationResult()
                    {
                        Result = AppResource.Translation_Yandex_BlockedInCountryError
                    };
                }
                else
                {
                    return new WordTranslationResult()
                    {
                        Error = ex,
                        Result = ""
                    };
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
                Result = translationResult,
                Synonyms = synonyms
            };
        }


        private Language _ParseLanguage()
        {
            var language = UserSettings.Translation.SelectedLanguage;

            if (Enum.TryParse(language, true, out Language supportedType))
                return supportedType;

            return Language.Uk;
        }
    }
}
