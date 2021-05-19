using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Repository.Interface;
using Read_and_learn.Service.Interface;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read_and_learn.Repository
{
    /// <summary>
    /// Implementation of <see cref="IBookmarkRepository"/>
    /// </summary>
    public class BookmarkRepository : IBookmarkRepository
    {
        SQLiteAsyncConnection _connection;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="databaseService"><see cref="IDatabaseService"/></param>
        public BookmarkRepository(IDatabaseService databaseService)
        {
            _connection = databaseService.Connection;
        }

        public Task<List<Bookmark>> GetBookmarksByBookIDAsync(Guid bookID)
            => _connection.Table<Bookmark>().Where(o => o.BookId == bookID).ToListAsync();

        public Task<int> DeleteBookmarkAsync(Bookmark bookmark)
            => _connection.DeleteAsync(bookmark);

        public Task<int> SaveBookmarkAsync(Bookmark item)
            => _connection.InsertOrReplaceAsync(item);
    }
}
