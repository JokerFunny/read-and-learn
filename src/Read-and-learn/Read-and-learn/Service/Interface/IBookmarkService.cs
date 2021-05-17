using Read_and_learn.Model.Bookshelf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface to handle work with <see cref="Bookmark"/>.
    /// </summary>
    public interface IBookmarkService
    {
        /// <summary>
        /// Delete bookmark via <paramref name="bookmark"/>
        /// </summary>
        /// <param name="bookmark">Bookmark to delete</param>
        /// <return>
        ///     True - if sucesfully, otherwise false.
        /// </return>
        bool DeleteBookmark(Bookmark bookmark);

        /// <summary>
        /// Load all bookmark for target book via <paramref name="bookId"/>.
        /// </summary>
        /// <param name="bookId">Id of target book</param>
        /// <returns>
        ///     List of all bookmarks if present, otherwise empty list.
        /// </returns>
        Task<List<Bookmark>> LoadBookmarksByBookId(Guid bookId);

        /// <summary>
        /// Create new bookmark with <paramref name="name"/> for target book via <paramref name="bookId"/> 
        /// and with specific <paramref name="position"/>.
        /// </summary>
        /// <param name="name">Target name</param>
        /// <param name="bookId">Id of target book</param>
        /// <param name="position">Target position</param>
        /// <return>
        ///     True - if sucesfully, otherwise false.
        /// </return>
        bool CreateBookmark(string name, Guid bookId, Position position);

        /// <summary>
        /// Save target <paramref name="bookmark"/>.
        /// </summary>
        /// <param name="bookmark">Target <see cref="Bookmark"/> to be saved</param>
        /// <return>
        ///     True - if sucesfully, otherwise false.
        /// </return>
        bool SaveBookmark(Bookmark bookmark);
    }
}
