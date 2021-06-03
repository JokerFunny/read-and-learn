using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Provider;
using Read_and_learn.Repository.Interface;
using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read_and_learn.Service
{
    /// <summary>
    /// Implementation of <see cref="IBookmarkService"/>.
    /// </summary>
    public class BookmarkService : IBookmarkService
    {
        IBookmarkRepository _bookmarkRepository;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="bookmarkRepository">Target <see cref="IBookmarkRepository"/></param>
        public BookmarkService(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public bool CreateBookmark(string name, string bookID, Position position)
        {
            var bookmark = new Bookmark
            {
                Id = BookmarkIdProvider.Id,
                Name = name,
                Position = new Position(position),
                BookId = bookID,
            };

            Task<bool> task = Task.Run(async () => await SaveBookmark(bookmark));

            return task.Result;
        }

        public async Task<bool> DeleteBookmark(Bookmark bookmark)
        {
            bookmark.Deleted = true;
            bookmark.Name = string.Empty;
            bookmark.Position = new Position();

            return await _bookmarkRepository.DeleteBookmarkAsync(bookmark) != 0;
        }

        public async Task<List<Bookmark>> LoadBookmarksByBookId(string bookId)
            => await _bookmarkRepository.GetBookmarksByBookIDAsync(bookId);

        public async Task<bool> SaveBookmark(Bookmark bookmark)
        {
            bookmark.LastChange = DateTimeOffset.UtcNow;

            return await _bookmarkRepository.SaveBookmarkAsync(bookmark) != 0;
        }
    }
}
