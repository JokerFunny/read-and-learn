namespace Read_and_learn.Model.View.Reader
{
    /// <summary>
    /// VM for reader settings.
    /// </summary>
    public class ReaderMenuSettingsVM
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
        /// Default ctor.
        /// </summary>
        public ReaderMenuSettingsVM() 
        {
            FontSize = new FontSizeVM();
            Margin = new MarginVM();
        }
    }
}
