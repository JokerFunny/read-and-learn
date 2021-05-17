using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Service.Interface;
using SQLite;

namespace Read_and_learn.Service
{
    /// <summary>
    /// Omplementation of <see cref="IDatabaseService"/>
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        SQLiteAsyncConnection _databaseConnection;

        public SQLiteAsyncConnection Connection
            => _databaseConnection;

        /// <summary>
        /// Default ctor. Initialize DB if not exist.
        /// </summary>
        /// <param name="fileHelper"><see cref="IFileHelper"/></param>
        public DatabaseService(IFileHelper fileHelper)
        {
            var dbPath = fileHelper.GetLocalFilePath(AppSettings.Bookshelft.SqlLiteFilename);
            _databaseConnection = new SQLiteAsyncConnection(dbPath);

            _CreateTables();
        }

        private void _CreateTables()
        {
            _databaseConnection.CreateTableAsync<Book>().Wait();
            _databaseConnection.CreateTableAsync<Bookmark>().Wait();
        }
    }
}
