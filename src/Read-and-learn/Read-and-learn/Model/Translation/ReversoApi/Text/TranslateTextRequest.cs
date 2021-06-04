namespace Read_and_learn.Model.Translation.ReversoApi.Text
{
    /// <summary>
    /// Nested class from <see cref="TranslateRequestBase"/> to hadnle operation with text part.
    /// </summary>
    public class TranslateTextRequest : TranslateRequestBase
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="from">Source language</param>
        /// <param name="to">Target language</param>
        public TranslateTextRequest(Language @from, Language to) : base(@from, to)
        { }
    }
}
