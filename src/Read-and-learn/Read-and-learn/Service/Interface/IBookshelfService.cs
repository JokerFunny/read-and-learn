using Read_and_learn.Model.Bookshelf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface to handle work with bookshelf.
    /// </summary>
    public interface IBookshelfService
    {
        /// <summary>
        /// Add new book via <paramref name="file"/>.
        /// </summary>
        /// <param name="file">Target file</param>
        /// <returns>
        ///     Tuple with book + add result.
        ///     Book - loaded book. If book already exist in system - return it.
        ///     Add result - if book already exist - false, otherwise true.
        /// </returns>
        Task<Tuple<Book, bool>> AddBook(FileResult file);

        /// <summary>
        /// Get list of all books.
        /// </summary>
        /// <returns>
        ///     <see cref="List{T}"/> of <see cref="Book"/>
        /// </returns>
        Task<List<Book>> LoadBooks();

        /// <summary>
        /// Remove book via <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Id of target book</param>
        /// <returns>
        ///     True - if sucesfully, otherwise false.
        /// </returns>
        Task<bool> RemoveById(string id);

        /// <summary>
        /// Save target <paramref name="book"/>.
        /// </summary>
        /// <param name="book">Target book to save</param>
        /// <returns>
        ///     True - if sucesfully, otherwise false.
        /// </returns>
        Task<bool> SaveBook(Book book);
    }
}
