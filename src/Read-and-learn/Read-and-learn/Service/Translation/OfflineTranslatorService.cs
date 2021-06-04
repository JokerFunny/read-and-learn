using Read_and_learn.Model.DataStructure;
using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace Read_and_learn.Service.Translation
{
    /// <summary>
    /// Implementation of <see cref="ITranslatorService"/> for <see cref="TranslationServicesProvider.Offline"/>.
    /// </summary>
    public class OfflineTranslatorService : ITranslatorService
    {
        private Dictionary<string, string> _availableTranslations = new Dictionary<string, string>();
        private readonly string _resourceFile = "Read_and_learn.Resources.OfflineDictionary.en-uk.xml";
        private const string _supportedLanguage = "en";
        private string[] _wordsSeparator = new string[] { ", " };

        /// <summary>
        /// Default ctor.
        /// </summary>
        public OfflineTranslatorService()
        {
            _LoadAvailableTranslations();
        }

        /// <remarks>
        /// NOT SUPPORTED FOR CURRENT MOMENT! WILL THROW EXCEPTION!!
        /// </remarks>
        public Task<TranslationResult> TranslatePart(string targetPart, string sourceLanguage)
        {
            throw new NotImplementedException();
        }

        public Task<WordTranslationResult> TranslateWord(string targetWord, string sourceLanguage)
        {
            var result = new TaskCompletionSource<WordTranslationResult>();

            if (sourceLanguage != _supportedLanguage)
            {
                result.SetResult(new WordTranslationResult()
                {
                    Result = "Source language not supported for translation."
                });

                return result.Task;
            }

            if (_availableTranslations.TryGetValue(targetWord.ToLower(), out string translationResult))
            {
                string targetTranslation;
                var synonyms = new List<string>();
                int nextWordStartIndex = translationResult.IndexOf(", ");

                if (nextWordStartIndex != -1)
                {
                    targetTranslation = translationResult.Substring(0, nextWordStartIndex);

                    synonyms.AddRange(translationResult
                        .Substring(nextWordStartIndex)
                        .Split(_wordsSeparator, StringSplitOptions.RemoveEmptyEntries));
                }
                else
                    targetTranslation = translationResult;

                result.SetResult(new WordTranslationResult()
                {
                    Result = targetTranslation,
                    Synonyms = synonyms
                });
            }
            else
            {
                result.SetResult(new WordTranslationResult()
                {
                    Result = "There is no available translation for target word :("
                });
            }

            return result.Task;
        }

        private void _LoadAvailableTranslations()
        {
            XmlDocument xDoc = new XmlDocument(); 
            
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ITranslatorService)).Assembly;
            Stream fileStream = assembly.GetManifestResourceStream(_resourceFile);
            xDoc.Load(fileStream);

            // add available translations to the inner dictionary.
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
                _availableTranslations.Add(xnode.FirstChild.InnerText, xnode.LastChild.InnerText);
        }
    }
}
