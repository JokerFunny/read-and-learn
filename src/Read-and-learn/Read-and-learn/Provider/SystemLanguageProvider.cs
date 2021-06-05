using System.Collections.Generic;
using System.Linq;

namespace Read_and_learn.Provider
{
    /// <summary>
    /// Provide available system languages.
    /// </summary>
    public class SystemLanguageProvider
    {
        /// <summary>
        /// Available languages for localization.
        /// </summary>
        public static Dictionary<string, string> Languages = new Dictionary<string, string>()
        {
            { "en-US", "English" },
            { "ru", "Русский" },
            { "uk", "Українська" }
        };

        /// <summary>
        /// Available system languages.
        /// </summary>
        public static List<string> Items => Languages.Values.ToList();
    }
}
