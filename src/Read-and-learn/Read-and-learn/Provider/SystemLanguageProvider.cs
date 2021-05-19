using System.Collections.Generic;

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
            { "en", "English" },
            { "ru", "Русский" },
            { "uk", "Українська" }
        };
    }
}
