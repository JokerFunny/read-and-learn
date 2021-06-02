using Read_and_learn.Model.DataStructure;
using Read_and_learn.Service.Interface;
using System;
using System.Threading.Tasks;

namespace Read_and_learn.Service.Translation
{
    /// <summary>
    /// Implementation of <see cref="ITranslatorService"/> for <see cref="TranslationServicesProvider.Offline"/>.
    /// </summary>
    public class OfflineTranslatorService : ITranslatorService
    {
        public Task<TranslationResult> TranslatePart(string targetPart, string sourceLanguage)
        {
            throw new NotImplementedException();
        }

        public Task<WordTranslationResult> TranslateWord(string targetWord, string sourceLanguage)
        {
            throw new NotImplementedException();
        }
    }
}
