using Autofac;
using Microsoft.AppCenter.Crashes;
using Read_and_learn.Model.DataStructure;
using Read_and_learn.Provider;
using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Read_and_learn.Service.Translation
{
    /// <summary>
    /// Implementation of <see cref="ITranslateService"/>
    /// </summary>
    public class GlobalTranslateService : ITranslateService
    {
        private ITranslatorService _offlineTranslator;

        /// <summary>
        /// Default ctor.
        /// </summary>
        public GlobalTranslateService()
        {
            _offlineTranslator = new OfflineTranslatorService();
        }

        public async Task<TranslationResult> TranslatePart(string targetPart, string sourceLanguage)
        {
            string currentProvider = UserSettings.Translation.Provider;
            ITranslatorService translatorProvider = _GetTranslator(currentProvider);

            if (!(translatorProvider is OfflineTranslatorService))
            {
                var result = await translatorProvider.TranslatePart(targetPart, sourceLanguage);

                if (result.Error != null)
                {
                    Crashes.TrackError(result.Error, new Dictionary<string, string> {
                        { "Provider", UserSettings.Translation.Provider },
                        { "Target part", targetPart },
                        { "Source language", sourceLanguage }
                    });
                }

                result.Provider = currentProvider;

                return result;
            }

            return new TranslationResult()
            {
                Error = new Exception("No internet connection")
            };
        }

        public async Task<WordTranslationResult> TranslateWord(string targetWord, string sourceLanguage)
        {
            string currentProvider = UserSettings.Translation.Provider;
            ITranslatorService translatorProvider = _GetTranslator(currentProvider);

            var result = await translatorProvider.TranslateWord(targetWord, sourceLanguage);

            if (result.Error != null)
            {
                Crashes.TrackError(result.Error, new Dictionary<string, string> {
                    { "Provider", UserSettings.Translation.Provider },
                    { "Target word", targetWord },
                    { "Source language", sourceLanguage }
                });

                if (!(translatorProvider is OfflineTranslatorService))
                {
                    result = await _offlineTranslator.TranslateWord(targetWord, sourceLanguage);

                    currentProvider = TranslationServicesProvider.Offline;
                }
            }

            result.Provider = translatorProvider is OfflineTranslatorService 
                ? TranslationServicesProvider.Offline 
                : currentProvider;

            return result;
        }

        private ITranslatorService _GetTranslator(string currentProvider)
        {
            // check connection
            var current = Connectivity.NetworkAccess;

            // if exist - resolve target online translator (use value from user settings)
            if (current == NetworkAccess.Internet)
            {
                return IocManager.Container.ResolveKeyed<ITranslatorService>(currentProvider);
            }
            // otherwise use offline translator
            else
                return _offlineTranslator;
        }
    }
}
