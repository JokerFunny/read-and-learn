using Microsoft.AppCenter.Analytics;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Read_and_learn.Provider;
using Xamarin.Forms;

namespace Read_and_learn
{
    /// <summary>
    /// Static class to store main app setting.
    /// </summary>
    public static class UserSettings
    {
        private static ISettings _appSettings => CrossSettings.Current;
        private const string _defaultLanguage = "en-US";

        /// <summary>
        /// Indicate if program run in a first time.
        /// </summary>
        public static bool FirstRun
        {
            get => _appSettings.GetValueOrDefault(CreateKey(nameof(FirstRun)), true);
            set => _appSettings.AddOrUpdateValue(CreateKey(nameof(FirstRun)), value);
        }

        /// <summary>
        /// User agree with collection of analitics data.
        /// </summary>
        public static bool AnalyticsAgreement
        {
            get => _appSettings.GetValueOrDefault(CreateKey(nameof(AnalyticsAgreement)), false);
            set
            {
                SetAnalytics(value);
                _appSettings.AddOrUpdateValue(CreateKey(nameof(AnalyticsAgreement)), value);
            }
        }

        /// <summary>
        /// Language of application interface.
        /// </summary>
        public static string AppLanguage
        {
            get => _appSettings.GetValueOrDefault(CreateKey(nameof(AppLanguage)), _defaultLanguage);
            set => _appSettings.AddOrUpdateValue(CreateKey(nameof(AppLanguage)), value);
        }

        /// <summary>
        /// Contains user settings related to the reader.
        /// </summary>
        public static class Reader
        {
            private static int _fontSizeDefault = Device.RuntimePlatform == Device.Android ? 20 : 40;
            private static int _marginDefault = 30;
            private static int _scrollSpeedDefault = 200;

            /// <summary>
            /// Font size.
            /// </summary>
            public static int FontSize
            {
                get => _appSettings.GetValueOrDefault(CreateKey(nameof(Reader), nameof(FontSize)), _fontSizeDefault);
                set => _appSettings.AddOrUpdateValue(CreateKey(nameof(Reader), nameof(FontSize)), value);
            }

            /// <summary>
            /// Margin.
            /// </summary>
            /// <remarks>
            /// [DN]: Определить а надо ли...
            /// </remarks>
            public static int Margin
            {
                get => _appSettings.GetValueOrDefault(CreateKey(nameof(Reader), nameof(Margin)), _marginDefault);
                set => _appSettings.AddOrUpdateValue(CreateKey(nameof(Reader), nameof(Margin)), value);
            }

            /// <summary>
            /// Speed of reader page scrolling.
            /// </summary>
            public static int ScrollSpeed
            {
                get => _appSettings.GetValueOrDefault(CreateKey(nameof(Reader), nameof(ScrollSpeed)), _scrollSpeedDefault);
                set => _appSettings.AddOrUpdateValue(CreateKey(nameof(Reader), nameof(ScrollSpeed)), value);
            }

            /// <summary>
            /// Night mode selected.
            /// </summary>
            public static bool NightMode
            {
                get => _appSettings.GetValueOrDefault(CreateKey(nameof(Reader), nameof(NightMode)), true);
                set => _appSettings.AddOrUpdateValue(CreateKey(nameof(Reader), nameof(NightMode)), value);
            }

            /// <summary>
            /// Open reader in fullscreen.
            /// </summary>
            public static bool Fullscreen
            {
                get => _appSettings.GetValueOrDefault(CreateKey(nameof(Reader), nameof(Fullscreen)), Device.RuntimePlatform == Device.Android);
                set => _appSettings.AddOrUpdateValue(CreateKey(nameof(Reader), nameof(Fullscreen)), value);
            }
        }

        /// <summary>
        /// Store user settings about translation.
        /// </summary>
        public static class Translation
        {
            private const string _defaultTranslationLanguage = "uk";

            /// <summary>
            /// Selected target language for translation.
            /// </summary>
            public static string SelectedLanguage
            {
                get => _appSettings.GetValueOrDefault(CreateKey(nameof(Translation), nameof(SelectedLanguage)), _defaultTranslationLanguage);
                set => _appSettings.AddOrUpdateValue(CreateKey(nameof(Translation), nameof(SelectedLanguage)), value);
            }

            /// <summary>
            /// Selected provider.
            /// </summary>
            /// <remarks>
            ///     for online translation only.
            /// </remarks>
            public static string Provider
            {
                get => _appSettings.GetValueOrDefault(CreateKey(nameof(Translation), nameof(Provider)), TranslationServicesProvider.Google);
                set => _appSettings.AddOrUpdateValue(CreateKey(nameof(Translation), nameof(Provider)), value);
            }
        }

        /// <summary>
        /// Store user settings related to the control.
        /// </summary>
        public static class Control
        {
            /// <summary>
            /// Checnge of brightness.
            /// </summary>
            public static BrightnessChange BrightnessChange
            {
                get => (BrightnessChange)_appSettings.GetValueOrDefault(CreateKey(nameof(Control), nameof(BrightnessChange)),
                    Device.RuntimePlatform == Device.Android ? (int)BrightnessChange.Left : (int)BrightnessChange.None);
                set => _appSettings.AddOrUpdateValue(CreateKey(nameof(Control), nameof(BrightnessChange)), (int)value);
            }

            /// <summary>
            /// Can user controll page change via volume buttons.
            /// </summary>
            public static bool VolumeButtons
            {
                get => _appSettings.GetValueOrDefault(CreateKey(nameof(Control), nameof(VolumeButtons)), false);
                set => _appSettings.AddOrUpdateValue(CreateKey(nameof(Control), nameof(VolumeButtons)), value);
            }
        }

        private async static void SetAnalytics(bool enabled)
            => await Analytics.SetEnabledAsync(enabled);

        private static string CreateKey(params string[] names)
            => string.Join(".", names);
    }
}
