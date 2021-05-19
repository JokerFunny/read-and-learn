using System.Collections.Generic;

namespace Read_and_learn.Provider
{
    /// <summary>
    /// Available translation services.
    /// </summary>
    public class TranslationServicesProvider
    {
        /// <summary>
        /// Offline.
        /// </summary>
        public const string Offline = "Offline";

        /// <summary>
        /// Yandex.NET.Translator, link: https://translate.yandex.ru/.
        /// </summary>
        /// <remarks>
        /// REPO: 
        /// </remarks>
        public const string Yandex_Translator = "Yandex Translator";

        /// <summary>
        /// ReversoAPI, link: https://www.reverso.net/text_translation.aspx.
        /// </summary>
        /// <remarks>
        /// REPO:
        /// </remarks>
        public const string Reverso = "Reverso Translation";

        /// <summary>
        /// Google's online language tools, link: https://translate.googleapis.com/.
        /// </summary>
        public const string Google = "Google";

        /// <summary>
        /// All available providers.
        /// </summary>
        public static List<string> Services { get; } = new List<string> 
            {
                Yandex_Translator,
                Reverso,
                Google
            };
    }
}
