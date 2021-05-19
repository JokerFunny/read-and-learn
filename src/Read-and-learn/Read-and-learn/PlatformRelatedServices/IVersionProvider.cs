namespace Read_and_learn.PlatformRelatedServices
{
    /// <summary>
    /// Interface for platform-specific realization of application version.
    /// </summary>
    public interface IVersionProvider
    {
        /// <summary>
        /// Provide application version.
        /// </summary>
        string AppVersion { get; }
    }
}
