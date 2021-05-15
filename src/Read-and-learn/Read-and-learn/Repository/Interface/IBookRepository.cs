using Read_and_learn.Model.Bookshelf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read_and_learn.Repository.Interface
{
    /// <summary>
    /// Interface for Bookmark repository.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Get all books in async way.
        /// </summary>
        /// <returns>
        ///     List of all books.
        /// </returns>
        Task<List<Book>> GetAllBooksAsync();

        /// <summary>
        /// Get book by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Id of target book</param>
        /// <returns>
        ///     Target book.
        /// </returns>
        Task<Book> GetBookByIDAsync(Guid id);

        /// <summary>
        /// Delete book in async way.
        /// </summary>
        /// <param name="book">Target book</param>
        /// <returns>
        ///     The number of rows deleted.
        /// </returns>
        Task<int> DeleteBookAsync(Book book);

        /// <summary>
        /// Save book in async way.
        /// </summary>
        /// <param name="item">Target book</param>
        /// <returns>
        ///     The number of rows modified.
        /// </returns>
        Task<int> SaveBookAsync(Book item);
    }
}
