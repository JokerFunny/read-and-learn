using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Provider;
using Read_and_learn.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for system language settings.
    /// </summary>
    public class SystemLanguageProviderVM : BaseVM
    {
        IMessageBus _messageBus;

        /// <summary>
        /// Available languages.
        /// </summary>
        public List<string> Items => SystemLanguageProvider.Items;

        /// <summary>
        /// Default ctor.
        /// </summary>
        public SystemLanguageProviderVM()
        {
            _messageBus = IocManager.Container.Resolve<IMessageBus>();
            //UserSettings.AppLanguage = "en-US";
        }

        /// <summary>
        /// Selected value.
        /// </summary>
        public string Value
        {
            get => SystemLanguageProvider.Languages[UserSettings.AppLanguage];
            set
            {
                string selectedLanguage = SystemLanguageProvider.Languages.FirstOrDefault(x => x.Value == value).Key;

                if (UserSettings.AppLanguage == selectedLanguage)
                    return;

                UserSettings.AppLanguage = selectedLanguage;
                OnPropertyChanged();
                _messageBus.Send(new ChangeApplicationLanguageMessage { Language = selectedLanguage });
            }
        }
    }
}
