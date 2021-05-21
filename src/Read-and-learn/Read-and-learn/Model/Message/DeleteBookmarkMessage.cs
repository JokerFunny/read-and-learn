using Read_and_learn.Model.Bookshelf;

namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicate when bookmark was deleted.
    /// </summary>
    public class DeleteBookmarkMessage
    {
        /// <summary>
        /// Target bookmark to delete.
        /// </summary>
        public Bookmark Bookmark { get; set; }
    }
}
