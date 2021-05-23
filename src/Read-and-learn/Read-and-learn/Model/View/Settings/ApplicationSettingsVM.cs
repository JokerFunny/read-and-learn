namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for application settings page.
    /// </summary>
    public class ApplicationSettingsVM : BaseVM
    {
        /// <summary>
        /// Analytics agreement value.
        /// </summary>
        public bool AnalyticsAgreement
        {
            get => UserSettings.AnalyticsAgreement;
            set
            {
                if (UserSettings.AnalyticsAgreement == value)
                    return;

                UserSettings.AnalyticsAgreement = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// <see cref="SystemLanguageProviderVM"/>.
        /// </summary>
        public SystemLanguageProviderVM SystemLanguageProvider { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public ApplicationSettingsVM()
        {
            SystemLanguageProvider = new SystemLanguageProviderVM();
        }
    }
}
