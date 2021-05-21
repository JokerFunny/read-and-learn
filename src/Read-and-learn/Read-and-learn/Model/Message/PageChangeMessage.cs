namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicates when page should be changed.
    /// </summary>
    public class PageChangeMessage
    {
        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Total amount of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Position of end for current page.
        /// </summary>
        public int Position { get; set; }
    }
}
