using System.Threading.Tasks;

namespace Read_and_learn.PlatformRelatedServices
{
    /// <summary>
    /// Interface for platform-specific work with assets files.
    /// </summary>
    public interface IAssetsManager
    {
        /// <summary>
        /// Get file contex in async way via <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">Target file name</param>
        /// <returns></returns>
        Task<string> GetFileContentAsync(string fileName);
    }
}
