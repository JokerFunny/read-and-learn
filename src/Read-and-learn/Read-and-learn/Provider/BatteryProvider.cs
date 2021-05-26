using Xamarin.Essentials;

namespace Read_and_learn.Provider
{
    /// <summary>
    /// Provider to get value related to device battery.
    /// </summary>
    public class BatteryProvider
    {
        /// <summary>
        /// Value of current battery level.
        /// </summary>
        public int RemainingChargePercent { get; } = (int)(Battery.ChargeLevel * 100);
    }
}
