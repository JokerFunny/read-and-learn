﻿using System.Collections.Generic;
using System.Linq;

namespace Read_and_learn.Provider
{
    /// <summary>
    /// Provides languages available for translation.
    /// </summary>
    public class LanguagesForTranslationProvider
    {
        /// <summary>
        /// The language to translation mode map.
        /// </summary>
        public static Dictionary<string, string> Languages = new Dictionary<string, string>()
        {
            { "af", "Afrikaans" },
            { "sq", "Albanian" },
            { "ar", "Arabic" },
            { "hy", "Armenian" },
            { "az", "Azerbaijani" },
            { "eu", "Basque" },
            { "be", "Belarusian" },
            { "bn", "Bengali" },
            { "bg", "Bulgarian" },
            { "ca", "Catalan" },
            { "zh-CN", "Chinese" },
            { "hr", "Croatian" },
            { "cs", "Czech" },
            { "da", "Danish" },
            { "nl", "Dutch" },
            { "en", "English" },
            { "eo", "Esperanto" },
            { "et", "Estonian" },
            { "tl", "Filipino" },
            { "fi", "Finnish" },
            { "fr", "French" },
            { "gl", "Galician" },
            { "de", "German" },
            { "ka", "Georgian" },
            { "el", "Greek" },
            { "ht", "Haitian Creole" },
            { "iw", "Hebrew" },
            { "hi", "Hindi" },
            { "hu", "Hungarian" },
            { "is", "Icelandic" },
            { "id", "Indonesian" },
            { "ga", "Irish" },
            { "it", "Italian" },
            { "ja", "Japanese" },
            { "ko", "Korean" },
            { "lo", "Lao" },
            { "la", "Latin" },
            { "lv", "Latvian" },
            { "lt", "Lithuanian" },
            { "mk", "Macedonian" },
            { "ms", "Malay" },
            { "mt", "Maltese" },
            { "no", "Norwegian" },
            { "fa", "Persian" },
            { "pl", "Polish" },
            { "pt", "Portuguese" },
            { "ro", "Romanian" },
            { "ru", "Russian" },
            { "sr", "Serbian" },
            { "sk", "Slovak" },
            { "sl", "Slovenian" },
            { "es", "Spanish" },
            { "sw", "Swahili" },
            { "sv", "Swedish" },
            { "ta", "Tamil" },
            { "te", "Telugu" },
            { "th", "Thai" },
            { "tr", "Turkish" },
            { "uk", "Ukrainian" },
            { "ur", "Urdu" },
            { "vi", "Vietnamese" },
            { "cy", "Welsh" },
            { "yi", "Yiddish" }
        };

        /// <summary>
        /// Available target languages for translation.
        /// </summary>
        public static List<string> Items => Languages.Values.ToList();
    }
}
