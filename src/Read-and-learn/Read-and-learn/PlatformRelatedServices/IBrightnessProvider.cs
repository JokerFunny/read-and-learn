namespace Read_and_learn.PlatformRelatedServices
{
    /// <summary>
    /// Interface to handle platform-specific work with device brightness.
    /// </summary>
    public interface IBrightnessProvider
    {
        /// <summary>
        /// Current brightness.
        /// </summary>
        float Brightness { get; set; }
    }
}
