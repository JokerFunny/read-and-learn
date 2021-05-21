using Read_and_learn.Model.Bookshelf;

namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicates when book opened.
    /// </summary>
    public class OpenBookMessage
    {
        /// <summary>
        /// Target book.
        /// </summary>
        public Book Book { get; set; }
    }
}
