using Xamarin.Forms;

namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for navigation buttons setting.
    /// </summary>
    public class VolumeButtonVM : BaseVM
    {
        /// <summary>
        /// Should be shown.
        /// </summary>
        public bool Show
            => Device.RuntimePlatform == Device.Android;

        /// <summary>
        /// Is navigation via volume buttons enabled.
        /// </summary>
        public bool Enabled
        {
            get => UserSettings.Control.VolumeButtons;
            set
            {
                if (UserSettings.Control.VolumeButtons == value)
                    return;

                UserSettings.Control.VolumeButtons = value;
                OnPropertyChanged();
            }
        }
    }
}
