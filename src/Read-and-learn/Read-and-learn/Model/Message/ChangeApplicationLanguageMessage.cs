namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicate when application language was changed.
    /// </summary>
    public class ChangeApplicationLanguageMessage
    {
        /// <summary>
        /// Target language.
        /// </summary>
        public string Language { get; set; }
    }
}
