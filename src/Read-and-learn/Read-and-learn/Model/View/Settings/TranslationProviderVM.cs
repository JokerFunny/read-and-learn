using Read_and_learn.Provider;
using System.Collections.Generic;

namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for translation services setting.
    /// </summary>
    public class TranslationProviderVM : BaseVM
    {
        /// <summary>
        /// Available languages.
        /// </summary>
        public List<string> Items => TranslationServicesProvider.Services;

        /// <summary>
        /// Selected value.
        /// </summary>
        public string Value
        {
            get => UserSettings.Translation.Provider;
            set
            {
                if (UserSettings.Translation.Provider == value)
                    return;

                UserSettings.Translation.Provider = value;
                OnPropertyChanged();
            }
        }
    }
}
