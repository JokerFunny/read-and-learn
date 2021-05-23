using Read_and_learn.Provider;
using System.Collections.Generic;
using System.Linq;

namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for target translation langiage settins.
    /// </summary>
    public class TranslationLanguageProviderVM : BaseVM
    {
        /// <summary>
        /// Available languages.
        /// </summary>
        public List<string> Items => LanguagesForTranslationProvider.Items;

        /// <summary>
        /// Selected value.
        /// </summary>
        public string Value
        {
            get => LanguagesForTranslationProvider.Languages[UserSettings.Translation.SelectedLanguage];
            set
            {
                string selectedLanguage = LanguagesForTranslationProvider.Languages.FirstOrDefault(x => x.Value == value).Key;

                if (UserSettings.Translation.SelectedLanguage == selectedLanguage)
                    return;

                UserSettings.Translation.SelectedLanguage = selectedLanguage;
                OnPropertyChanged();
            }
        }
    }
}
