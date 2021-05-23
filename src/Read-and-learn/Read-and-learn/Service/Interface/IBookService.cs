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
        /// Open book via <paramref name="path"/>.
        /// </summary>
        /// <param name="targetFile">Target file</param>
        /// <returns>
        ///     <see cref="Ebook"/>.
        /// </returns>
        Task<Ebook> OpenBook(FileResult targetFile);

        /// <summary>
        /// Prepare formatted data from <paramref name="book"/>.
        /// </summary>
        /// <param name="book">Target book to parse</param>
        /// <returns>
        ///     <see cref="FormattedBook"/>.
        /// </returns>
        Task<FormattedBook> PrepareFormattedData(Ebook book);

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
