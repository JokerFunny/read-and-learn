namespace Read_and_learn.Model.Translation.ReversoApi.Word
{
    /// <summary>
    /// Nested class from <see cref="TranslateRequestBase"/> to hadnle operation with single word.
    /// </summary>
    public class TranslateWordRequest : TranslateRequestBase
    {
        /// <summary>
        /// Target word.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Word position.
        /// </summary>
        public string WordPos { get; set; } = "0";

        /// <summary>
        /// Page url.
        /// </summary>
        public const string PageUrl = "0";

        /// <summary>
        /// Page title.
        /// </summary>
        public const string PageTitle = "0";

        /// <summary>
        /// Reverso page.
        /// </summary>
        public const string ReversoPage = "null";

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="from">Original language</param>
        /// <param name="to">Target language</param>
        public TranslateWordRequest(Language @from, Language to) : base(@from, to)
        { }
    }
}
