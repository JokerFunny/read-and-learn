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
    }
}
