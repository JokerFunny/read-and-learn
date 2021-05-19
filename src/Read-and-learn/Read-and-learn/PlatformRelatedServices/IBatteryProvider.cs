namespace Read_and_learn.PlatformRelatedServices
{
    /// <summary>
    /// Interface to handle platform-specific work with device battery.
    /// </summary>
    public interface IBatteryProvider
    {
        /// <summary>
        /// Value of current battery level.
        /// </summary>
        int RemainingChargePercent { get; }
    }
}
