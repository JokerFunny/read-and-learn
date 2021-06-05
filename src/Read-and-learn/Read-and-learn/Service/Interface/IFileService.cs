using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface to hadle work with file(s).
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Create a local copy of file via <paramref name="fileDate"/>.
        /// </summary>
        /// <param name="fileDate">Target data</param>
        /// <param name="id">Target id</param>
        /// <returns>
        ///     File full path.
        /// </returns>
        Task<string> CreateLocalCopy(Stream fileDate, string id);

        /// <summary>
        /// Get file data via <paramref name="folderName"/>.
        /// </summary>
        /// <param name="folderName">Target folder name</param>
        /// <returns>
        ///     File content.
        /// </returns>
        Task<string> ReadFileContent(string folderName);

        /// <summary>
        /// Delete local folder via <paramref name="path"/>.
        /// </summary>
        /// <param name="path">Target folder path</param>
        /// <returns>
        ///     True if folder deleted, otherwise false.
        /// </returns>
        Task<bool> DeleteFolder(string path);

        /// <summary>
        /// Get array of bytes from target <paramref name="targetFile"/>.
        /// </summary>
        /// <param name="targetFile">Target <see cref="FileResult"/></param>
        /// <returns>
        ///     Byte data array.
        /// </returns>
        Task<byte[]> GetByteArrayFromFile(FileResult targetFile);
    }
}
