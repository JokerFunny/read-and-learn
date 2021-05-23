namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for control settings.
    /// </summary>
    public class ControlSettingsVM
    {
        /// <summary>
        /// <see cref="PanBrightnessChangeVM"/>.
        /// </summary>
        public PanBrightnessChangeVM PanBrightnessChange { get; set; }

        /// <summary>
        /// <see cref="VolumeButtonVM"/>
        /// </summary>
        public VolumeButtonVM VolumeButton { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public ControlSettingsVM()
        {
            PanBrightnessChange = new PanBrightnessChangeVM();
            VolumeButton = new VolumeButtonVM();
        }
    }
}
