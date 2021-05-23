using Read_and_learn.Provider;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for brightness settings.
    /// </summary>
    public class PanBrightnessChangeVM : BaseVM
    {
        /// <summary>
        /// Should this setting be showed.
        /// </summary>
        public bool Show
            => Device.RuntimePlatform == Device.Android;

        /// <summary>
        /// Available items.
        /// </summary>
        public List<string> Items => BrightnessChangeProvider.Items;

        /// <summary>
        /// Selected value.
        /// </summary>
        public string Value
        {
            get => UserSettings.Control.BrightnessChange.ToString();
            set
            {
                if (Enum.TryParse(value, out BrightnessChange enumValue))
                {
                    if (UserSettings.Control.BrightnessChange == enumValue)
                        return;

                    UserSettings.Control.BrightnessChange = enumValue;
                    OnPropertyChanged();
                }
            }
        }
    }
}
