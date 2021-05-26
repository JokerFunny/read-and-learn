namespace Read_and_learn.Model.DataStructure
{
    /// <summary>
    /// Model for navigation item.
    /// </summary>
    public class Navigation
    {
        /// <summary>
        /// Id of current navigation element.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Target position in book.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Depth of target element.
        /// </summary>
        public int Depth { get; set; }
    }
}
