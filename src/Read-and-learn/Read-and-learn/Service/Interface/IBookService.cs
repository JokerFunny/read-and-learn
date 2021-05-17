using Read_and_learn.Model;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.DataStructure;
using System.Threading.Tasks;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface for handle work with <see cref="Ebook"/>.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Get <see cref="Ebook"/> via <paramref name="fileName"/>, <paramref name="fileData"/> and <paramref name="bookId"/>.
        /// </summary>
        /// <param name="fileName">Target file name</param>
        /// <param name="fileData">Target file data content</param>
        /// <param name="bookId">Target book id</param>
        /// <returns>
        ///     <see cref="Ebook"/>.
        /// </returns>
        Task<Ebook> GetBook(string fileName, byte[] fileData, string bookId);

        /// <summary>
        /// Open book via <paramref name="path"/>.
        /// </summary>
        /// <param name="path">Target path</param>
        /// <returns>
        ///     <see cref="Ebook"/>.
        /// </returns>
        Task<Ebook> OpenBook(string path);

        /// <summary>
        /// Get chapter content from <paramref name="book"/> via <paramref name="targetSection"/>.
        /// </summary>
        /// <param name="book">Target book</param>
        /// <param name="targetSection">Target section</param>
        /// <returns>
        ///     Section content.
        /// </returns>
        Task<string> GetSectionContent(Ebook book, Section targetSection);

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
