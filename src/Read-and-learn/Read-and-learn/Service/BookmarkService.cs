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

        public bool CreateBookmark(string name, string bookId, Position position)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(bookId))
                throw new ArgumentNullException(nameof(bookId));
            if (position == null)
                throw new ArgumentNullException(nameof(position));

            var bookmark = new Bookmark
            {
                Id = BookmarkIdProvider.Id,
                Name = name,
                Position = new Position(position),
                BookId = bookId,
            };

            Task<bool> task = Task.Run(async () => await SaveBookmark(bookmark));

            return task.Result;
        }

        public async Task<bool> DeleteBookmark(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException(nameof(bookmark));

            bookmark.Deleted = true;
            bookmark.Name = string.Empty;
            bookmark.Position = new Position();

            return await _bookmarkRepository.DeleteBookmarkAsync(bookmark) != 0;
        }

        public async Task<List<Bookmark>> LoadBookmarksByBookId(string bookId)
        {
            if (string.IsNullOrEmpty(bookId))
                throw new ArgumentNullException(nameof(bookId));

            return await _bookmarkRepository.GetBookmarksByBookIdAsync(bookId);
        }

        public async Task<bool> SaveBookmark(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException(nameof(bookmark));

            bookmark.LastChange = DateTimeOffset.UtcNow;

            return await _bookmarkRepository.SaveBookmarkAsync(bookmark) != 0;
        }
    }
}
