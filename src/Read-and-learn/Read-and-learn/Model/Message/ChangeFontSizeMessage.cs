namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicate when font size was changed.
    /// </summary>
    public class ChangeFontSizeMessage
    {
        /// <summary>
        /// Target font size.
        /// </summary>
        public int FontSize { get; set; }
    }
}
