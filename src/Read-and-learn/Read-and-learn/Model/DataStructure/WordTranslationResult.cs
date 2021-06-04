using System.Collections.Generic;

namespace Read_and_learn.Model.DataStructure
{
    /// <summary>
    /// Model to handle result of the word translation.
    /// </summary>
    public class WordTranslationResult : TranslationResult
    {
        /// <summary>
        /// Synonyms for translation.
        /// </summary>
        public List<string> Synonyms { get; set; }

        /// <summary>
        /// Transcription of target word.
        /// </summary>
        public string Transcription { get; set; }

        /// <summary>
        /// Context of target word using.
        /// </summary>
        public List<string> Contexts { get; set; }

        /// <summary>
        /// Get <see cref="WordTranslationResult"/> for <paramref name="translationResult"/>.
        /// </summary>
        /// <param name="translationResult">Target <see cref="TranslationResult"/></param>
        /// <returns>
        ///     Proper <see cref="WordTranslationResult"/>.
        /// </returns>
        public static WordTranslationResult GetWordTranslationResult(TranslationResult translationResult)
        {
            return new WordTranslationResult()
            {
                Result = translationResult.Result,
                Error = translationResult.Error,
                Provider = translationResult.Provider
            };
        }
    }
}
