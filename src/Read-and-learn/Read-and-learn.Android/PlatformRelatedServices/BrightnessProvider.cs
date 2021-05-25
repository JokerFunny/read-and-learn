using Read_and_learn.PlatformRelatedServices;

namespace Read_and_learn.Droid.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IBrightnessProvider"/>.
    /// </summary>
    public class BrightnessProvider : IBrightnessProvider
    {
        public float Brightness { get; set; }
    }
}