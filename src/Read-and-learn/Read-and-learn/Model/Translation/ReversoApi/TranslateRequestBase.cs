namespace Read_and_learn.Model.Translation.ReversoApi
{
    /// <summary>
    /// Base class for requests.
    /// </summary>
    public class TranslateRequestBase
    {
        /// <summary>
        /// Device Id.
        /// </summary>
        public const string DeviceId = "0";

        /// <summary>
        /// UI language (need for request).
        /// </summary>
        public const string UiLang = "ru";

        /// <summary>
        /// Origin of request.
        /// </summary>
        public const string Origin = "chromeextension";

        /// <summary>
        /// Access token.
        /// </summary>
        public const string AccessToken = "";

        /// <summary>
        /// Direction of translation.
        /// </summary>
        public readonly string Direction;

        /// <summary>
        /// Target text to be translated.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Application Id.
        /// </summary>
        public const string AppId = "0";

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="from">Original language</param>
        /// <param name="to">Target language</param>
        public TranslateRequestBase(Language from, Language to)
        {
            Direction = $"{from.ToString().ToLower()}-{to.ToString().ToLower()}";
        }
    }
}
