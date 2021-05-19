using System.Collections.Generic;

namespace Read_and_learn.Provider
{
    /// <summary>
    /// Provide supported margin values.
    /// </summary>
    public static class MarginProvider
    {
        /// <summary>
        /// Supported margins.
        /// </summary>
        public static List<int> Margins { get; } = new List<int> 
            {
                15,
                30,
                45,
            };
    }
}
