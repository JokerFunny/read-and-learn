using Read_and_learn.Model.DataStructure;
using System.Threading.Tasks;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface to handle work with translation providers.
    /// </summary>
    public interface ITranslatorService
    {
        /// <summary>
        /// Translate <paramref name="targetWord"/> using <paramref name="sourceLanguage"/>.
        /// and specified by user target language.
        /// </summary>
        /// <param name="targetWord">Target word to translate</param>
        /// <param name="sourceLanguage">Word source language</param>
        /// <returns>
        ///     Result of translation, <see cref="WordTranslationResult"/>.
        /// </returns>
        Task<WordTranslationResult> TranslateWord(string targetWord, string sourceLanguage);

        /// <summary>
        /// Translate <paramref name="targetPart"/> using <paramref name="sourceLanguage"/>.
        /// </summary>
        /// <param name="targetPart">Target part to translate</param>
        /// <param name="sourceLanguage">Word source language</param>
        /// <returns>
        ///     Result of translation, <see cref="TranslationResult"/>.
        /// </returns>
        /// <remarks>
        ///     The target part is meant as a phrase, part of a sentence, sentence, or even more, for example, a paragraph of text.
        /// </remarks>
        Task<TranslationResult> TranslatePart(string targetPart, string sourceLanguage);
    }
}
