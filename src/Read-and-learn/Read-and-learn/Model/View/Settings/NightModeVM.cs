namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for night mode settings.
    /// </summary>
    public class NightModeVM : BaseVM
    {
        /// <summary>
        /// Is night mode enabled.
        /// </summary>
        public bool Enabled
        {
            get => UserSettings.Reader.NightMode;
            set
            {
                if (UserSettings.Reader.NightMode == value)
                    return;

                UserSettings.Reader.NightMode = value;
                OnPropertyChanged();
            }
        }
    }
}
