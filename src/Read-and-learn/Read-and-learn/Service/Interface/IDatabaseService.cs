using SQLite;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface for handle work with DB.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Instance of <see cref="SQLiteAsyncConnection"/>.
        /// </summary>
        SQLiteAsyncConnection Connection { get; }
    }
}
