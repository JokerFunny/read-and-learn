using System.Collections.Generic;
using System.Linq;

namespace Read_and_learn.Provider
{
    /// <summary>
    /// Provide available font sizes.
    /// </summary>
    public static class FontSizeProvider
    {
        private const int _minValue = 12;
        private const int _maxValue = 80;

        /// <summary>
        /// Supported font sizes.
        /// </summary>
        public static List<int> Sizes
            => Enumerable.Range(_minValue, _maxValue - _minValue + 1).Where(o => o % 2 == 0).ToList();
    }
}
