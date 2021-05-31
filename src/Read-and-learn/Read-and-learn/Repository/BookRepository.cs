using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Repository.Interface;
using Read_and_learn.Service.Interface;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read_and_learn.Repository
{
    /// <summary>
    /// Implementation of <see cref="IBookRepository"/>
    /// </summary>
    public class BookRepository : IBookRepository
    {
        SQLiteAsyncConnection _connection;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="databaseService"><see cref="IDatabaseService"/></param>
        public BookRepository(IDatabaseService databaseService)
        {
            _connection = databaseService.Connection;
        }

        public Task<List<Book>> GetAllBooksAsync()
            => _connection.Table<Book>().ToListAsync();

        public Task<Book> GetBookByIdAsync(string id)
            => _connection.Table<Book>().Where(i => i.Id == id).FirstOrDefaultAsync();

        public Task<int> DeleteBookAsync(Book book)
            => _connection.DeleteAsync(book);

        public Task<int> SaveBookAsync(Book item)
            => _connection.InsertOrReplaceAsync(item);
    }
}
