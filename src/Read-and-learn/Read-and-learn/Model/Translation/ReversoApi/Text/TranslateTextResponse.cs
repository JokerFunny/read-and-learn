namespace Read_and_learn.Model.Translation.ReversoApi.Text
{
    /// <summary>
    /// Model to handle responce of text part translation.
    /// </summary>
    public class TranslateTextResponse : ResporseError
    {
        /// <summary>
        /// Target translation.
        /// </summary>
        public string Translation { get; set; }

        /// <summary>
        /// Favourite id.
        /// </summary>
        public object FavoriteId { get; set; }

        /// <summary>
        /// Source language.
        /// </summary>
        public string DirectionFrom { get; set; }

        /// <summary>
        /// Target language.
        /// </summary>
        public string DirectionTo { get; set; }

        /// <summary>
        /// Is direction was changed.
        /// </summary>
        public bool IsDirectionChanged { get; set; }
    }
}
