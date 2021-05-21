namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicate when brightness was changed.
    /// </summary>
    public class ChangeBrightnessMessage
    {
        /// <summary>
        /// Target brightness.
        /// </summary>
        public float Brightness { get; set; }
    }
}
