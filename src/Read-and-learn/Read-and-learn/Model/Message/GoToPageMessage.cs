namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicate when navigation buttons clicked.
    /// </summary>
    public class GoToPageMessage
    {
        /// <summary>
        /// Target page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Go to next page.
        /// </summary>
        public bool Next { get; set; }

        /// <summary>
        /// Go to previous page.
        /// </summary>
        public bool Previous { get; set; }
    }
}
