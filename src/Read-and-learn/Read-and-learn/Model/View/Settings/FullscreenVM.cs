namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for fullscrean setting.
    /// </summary>
    public class FullscreenVM : BaseVM
    {
        /// <summary>
        /// Is fullscreen enabled.
        /// </summary>
        public bool Enabled
        {
            get => UserSettings.Reader.Fullscreen;
            set
            {
                if (UserSettings.Reader.Fullscreen == value)
                    return;

                UserSettings.Reader.Fullscreen = value;
                OnPropertyChanged();
            }
        }
    }
}
