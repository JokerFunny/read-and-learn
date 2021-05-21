using Read_and_learn.Model.Bookshelf;

namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicates when bookmark opened.
    /// </summary>
    public class OpenBookmarkMessage
    {
        /// <summary>
        /// Target bookmark.
        /// </summary>
        public Bookmark Bookmark { get; set; }
    }
}
