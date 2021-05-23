namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for translation settings.
    /// </summary>
    public class TransltaionSettingsVM
    {
        /// <summary>
        /// <see cref="TranslationLanguageProviderVM"/>.
        /// </summary>
        public TranslationLanguageProviderVM TranslationLanguageProvider { get; set; }

        /// <summary>
        /// <see cref="TranslationProviderVM"/>.
        /// </summary>
        public TranslationProviderVM TranslationProvider { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public TransltaionSettingsVM()
        {
            TranslationLanguageProvider = new TranslationLanguageProviderVM();
            TranslationProvider = new TranslationProviderVM();
        }
    }
}
