using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface for handle work with cloud storage.
    /// </summary>
    public interface ICloudStorageService
    {
        /// <summary>
        /// Check if connection could be established using provided credentials.
        /// </summary>
        /// <returns>
        ///     True - if connection could be established, otherwise false.
        /// </returns>
        bool IsConnected();

        /// <summary>
        /// Save <paramref name="json"/> under specific <paramref name="path"/> on cloud.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="json"/></typeparam>
        /// <param name="json">Target json</param>
        /// <param name="path">Target path</param>
        /// <returns>
        ///     True - if data saved, otherwise false.
        /// </returns>
        bool SaveJson<T>(T json, string[] path);

        /// <summary>
        /// Get data from cloud via <paramref name="path"/>.
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="path">Target path</param>
        /// <returns>
        ///     Data - if data could be loaded, otherwise default for <typeparamref name="T"/>.
        /// </returns>
        Task<T> LoadJson<T>(string[] path);

        /// <summary>
        /// Get data by <paramref name="path"/> using <see cref="LoadJson{T}(string[])"/>.
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="path">Target paths</param>
        /// <returns>
        ///     Data - if data could be loaded, otherwise default for <typeparamref name="T"/>.
        /// </returns>
        Task<List<T>> LoadJsonList<T>(string[] path);

        /// <summary>
        /// Delete file or folder in cloud via <paramref name="path"/>.
        /// </summary>
        /// <param name="path">Target path</param>
        /// <returns>
        ///     True - if data deleted, otherwise false.
        /// </returns>
        bool DeleteNode(string[] path);
    }
}
