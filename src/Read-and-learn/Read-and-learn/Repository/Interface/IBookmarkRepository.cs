using Read_and_learn.Model.Bookshelf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read_and_learn.Repository.Interface
{
    /// <summary>
    /// Interface for Bookmark repository.
    /// </summary>
    public interface IBookmarkRepository
    {
        /// <summary>
        /// Get all bookmarks for book by <paramref name="bookId"/>.
        /// </summary>
        /// <param name="bookId">Id of target book</param>
        /// <returns>
        ///     List of all bookmarks for target book.
        /// </returns>
        Task<List<Bookmark>> GetBookmarksByBookIdAsync(string bookId);

        /// <summary>
        /// Delete bookmark in async way.
        /// </summary>
        /// <param name="book">Target bookmark</param>
        /// <returns>
        ///     The number of rows deleted.
        /// </returns>
        Task<int> DeleteBookmarkAsync(Bookmark bookmark);

        /// <summary>
        /// Save bookmark in async way.
        /// </summary>
        /// <param name="item">Target bookmark</param>
        /// <returns>
        ///     The number of rows modified.
        /// </returns>
        Task<int> SaveBookmarkAsync(Bookmark item);
    }
}
