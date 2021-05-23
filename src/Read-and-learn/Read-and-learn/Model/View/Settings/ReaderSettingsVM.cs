namespace Read_and_learn.Model.View.Settings
{
    /// <summary>
    /// VM for reader settings.
    /// </summary>
    public class ReaderSettingsVM
    {
        /// <summary>
        /// <see cref="FontSizeVM"/>
        /// </summary>
        public FontSizeVM FontSize { get; set; }

        /// <summary>
        /// <see cref="MarginVM"/>
        /// </summary>
        public MarginVM Margin { get; set; }

        /// <summary>
        /// <see cref="ScrollSpeedVM"/>
        /// </summary>
        public ScrollSpeedVM ScrollSpeed { get; set; }

        /// <summary>
        /// <see cref="NightModeVM"/>
        /// </summary>
        public NightModeVM NightMode { get; set; }

        /// <summary>
        /// <see cref="FullscreenVM"/>
        /// </summary>
        public FullscreenVM Fullscreen { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public ReaderSettingsVM()
        {
            FontSize = new FontSizeVM();
            Margin = new MarginVM();
            ScrollSpeed = new ScrollSpeedVM();
            NightMode = new NightModeVM();
            Fullscreen = new FullscreenVM();
        }
    }
}
