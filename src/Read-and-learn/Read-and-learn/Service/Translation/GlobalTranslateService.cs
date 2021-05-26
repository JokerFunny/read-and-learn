using Read_and_learn.Model.DataStructure;
using Read_and_learn.Service.Interface;
using System;
using System.Threading.Tasks;

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

        public Task<TranslationResult> TranslatePart(string targetPart, string sourceLanguage)
        {
            // check connection
            // if exist - resolve target online translator (use value from user settings)
            // otherwise use offline translator
            throw new NotImplementedException();
        }

        public Task<WordTranslationResult> TranslateWord(string targetWord, string sourceLanguage)
        {
            // check connection
            // if exist - resolve target online translator (use value from user settings)
            // otherwise use offline translator
            throw new NotImplementedException();
        }
    }
}
