using PCLAppConfig;

namespace Read_and_learn
{
    /// <summary>
    /// Static class to store main app setting.
    /// </summary>
    public static class AppSettings
    {
        public static string Color = "#FFA500";

        /// <summary>
        /// Store secret key for application centre.
        /// </summary>
        /// <remarks>
        ///     TO BE DONE!!!
        /// </remarks>
        public static class AppCenter
        {
            /// <summary>
            /// Secret key for Android.
            /// </summary>
            /// <remarks>
            ///     TO BE DETERMINED.
            /// </remarks>
            public static string Android => ConfigurationManager.AppSettings["AppCenter_Android"];

            /// <summary>
            /// Secret key for Apple.
            /// </summary>
            /// <remarks>
            ///     TO BE DETERMINED.
            ///     IF APPLE APP WILL BE DONE.
            /// </remarks>
            public static string Apple => ConfigurationManager.AppSettings["AppCenter_Apple"];

            /// <summary>
            /// Secret key for Windows.
            /// </summary>
            /// <remarks>
            ///     TO BE DETERMINED.
            /// </remarks>
            public static string UWP => ConfigurationManager.AppSettings["AppCenter_UWP"];
        }

        /// <summary>
        /// Hold settings about bookshelf.
        /// </summary>
        public static class Bookshelft
        {
            /// <summary>
            /// Name of DB file.
            /// </summary>
            public static string SqlLiteFilename = "bookshelf.db3";
        }
    }
}
