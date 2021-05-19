using System.Collections.Generic;

namespace Read_and_learn.Provider
{
    /// <summary>
    /// Provide available speed for page scrolling.
    /// </summary>
    public class ScrollSpeedProvider
    {
        /// <summary>
        /// Available speeds.
        /// </summary>
        public static List<int> Speeds { get; } = new List<int>() 
            {
                0,
                200,
                500,
            };
    }
}
