using Read_and_learn.Model;
using Read_and_learn.Model.Bookshelf;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface for handle work with <see cref="Ebook"/>.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Open book via <paramref name="bookId"/> and <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">Target fuul path</param>
        /// <param name="bookId">Target book</param>
        /// <returns>
        ///     <see cref="Ebook"/>.
        /// </returns>
        Task<Ebook> OpenBook(string fullPath, string bookId);

        /// <summary>
        /// Get book via <paramref name="targetStream"/>.
        /// </summary>
        /// <param name="targetFile">Target file</param>
        /// <param name="bookId">Target id</param>
        /// <returns>
        ///     <see cref="Ebook"/>.
        /// </returns>
        /// <remarks>
        ///     Create local copy of target book.
        /// </remarks>
        Task<Ebook> GetBook(FileResult targetFile, string bookId);

        /// <summary>
        /// Create bookself book <see cref="Book"/> for target <paramref name="book"/>.
        /// </summary>
        /// <param name="book">Target <see cref="Ebook"/></param>
        /// <returns>
        ///     <see cref="Book"/>.
        /// </returns>
        Book CreateBookshelfBook(Ebook book);
    }
}
