using System.Collections.Generic;

namespace Read_and_learn.Model.Translation.ReversoApi
{
    /// <summary>
    /// Base class for translation responce.
    /// </summary>
    public class TranslatedResponse : ResporseError
    {
        /// <summary>
        /// List of <see cref="Sources"/>.
        /// </summary>
        public IList<Sources> Sources { get; set; }

        /// <summary>
        /// Word sentence.
        /// </summary>
        public string WordSentence { get; set; }
    }

    /// <summary>
    /// Model to handle context.
    /// </summary>
    public class Context
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public bool IsGood { get; set; }
    }

    /// <summary>
    /// Model to handle result of tranlation.
    /// </summary>
    public class Translations
    {
        public string Translation { get; set; }
        public int Count { get; set; }
        public IList<Context> Contexts { get; set; }
        public bool IsFromDict { get; set; }
        public string Pos { get; set; }
        public bool IsRude { get; set; }
        public bool IsSlang { get; set; }
        public bool IsReverseValidated { get; set; }
        public bool IsGrayed { get; set; }
        public object FavoriteId { get; set; }
    }

    /// <summary>
    /// Model to handle sources.
    /// </summary>
    public class Sources
    {
        public int Count { get; set; }
        public string Source { get; set; }
        public string DisplaySource { get; set; }
        public IList<Translations> Translations { get; set; }
        public bool SpellCorrected { get; set; }
        public string DirectionFrom { get; set; }
        public string DirectionTo { get; set; }
    }
}
