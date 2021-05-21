using Read_and_learn.Model.Bookshelf;

namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicate when book was deleted.
    /// </summary>
    public class DeleteBookMessage
    {
        /// <summary>
        /// Target book to delete.
        /// </summary>
        public Book Book { get; set; }
    }
}
