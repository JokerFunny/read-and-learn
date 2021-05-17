using PCLStorage;
using System.Threading.Tasks;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface to hadle work with file(s).
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Open file via <paramref name="name"/> and <paramref name="folder"/>.
        /// </summary>
        /// <param name="name">Target file name</param>
        /// <param name="folder">Target folder</param>
        /// <returns>
        ///     <see cref="IFile"/> or null if file doesn`t exist.
        /// </returns>
        Task<IFile> OpenFile(string name, IFolder folder);

        /// <summary>
        /// Get local file name via <paramref name="path"/>.
        /// </summary>
        /// <param name="path">Target path</param>
        /// <returns>
        ///     Local file name.
        /// </returns>
        string GetLocalFileName(string path);

        /// <summary>
        /// Get file data via <paramref name="fileName"/> and <paramref name="folder"/>.
        /// </summary>
        /// <param name="fileName">Target file name</param>
        /// <param name="folder">Target folder name</param>
        /// <returns>
        ///     File content or null if file doesn`t exist.
        /// </returns>
        Task<string> ReadFileData(string fileName, IFolder folder);

        /// <summary>
        /// Save target file via <paramref name="path"/> and <paramref name="content"/>.
        /// </summary>
        /// <param name="path">Target path</param>
        /// <param name="content">Target file content</param>
        /// <returns>
        ///     True if file saved, otherwise false.
        /// </returns>
        bool Save(string path, string content);

        /// <summary>
        /// Chech if target file exist in local storage.
        /// </summary>
        /// <param name="fileName">Target file name</param>
        /// <returns>
        ///     True if file exist, otherwise false.
        /// </returns>
        Task<bool> CheckFile(string fileName);

        /// <summary>
        /// Delete local folder via <paramref name="path"/>.
        /// </summary>
        /// <param name="path">Target folder path</param>
        /// <returns>
        ///     True if folder deleted, otherwise false.
        /// </returns>
        bool DeleteFolder(string path);
    }
}
