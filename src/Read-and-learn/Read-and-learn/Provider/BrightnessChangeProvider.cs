using System.Collections.Generic;

namespace Read_and_learn.Provider
{
    /// <summary>
    /// Provide supported brightness change.
    /// </summary>
    public static class BrightnessChangeProvider
    {
        /// <summary>
        /// List of supported changes.
        /// </summary>
        public static List<string> Items { get; } = new List<string> 
            {
                BrightnessChange.Left.ToString(),
                BrightnessChange.Right.ToString(),
                BrightnessChange.None.ToString(),
            };
    }

    /// <summary>
    /// Specify change of brightness using slider.
    /// </summary>
    public enum BrightnessChange
    {
        Left,
        Right,
        None
    }
}
