namespace Read_and_learn.PlatformRelatedServices
{
    /// <summary>
    /// Interface for platform-specific work with files.
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Get local file path for target <paramref name="filename"/>.
        /// </summary>
        /// <param name="filename">Target file name</param>
        /// <returns>
        ///     Local path to the target file.
        /// </returns>
        string GetLocalFilePath(string filename);
    }
}
