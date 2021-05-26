using Read_and_learn.Model.Bookshelf;

namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicate when bookmark name was changed.
    /// </summary>
    public class BookmarkNameChangedMessage
    { 
        /// <summary>
        /// Target bookmark.
        /// </summary>
        public Bookmark Bookmark { get; set; }
    }
}
