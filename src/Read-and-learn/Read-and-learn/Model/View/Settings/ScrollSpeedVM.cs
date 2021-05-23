using Read_and_learn.Provider;
using System.Collections.Generic;

namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for page scroll speed setting.
    /// </summary>
    public class ScrollSpeedVM : BaseVM
    {
        /// <summary>
        /// Available speeds.
        /// </summary>
        public List<int> Items => ScrollSpeedProvider.Speeds;

        /// <summary>
        /// Selected value.
        /// </summary>
        public int Value
        {
            get => UserSettings.Reader.ScrollSpeed;
            set
            {
                if (UserSettings.Reader.ScrollSpeed == value)
                    return;

                UserSettings.Reader.ScrollSpeed = value;
                OnPropertyChanged();
            }
        }
    }
}
