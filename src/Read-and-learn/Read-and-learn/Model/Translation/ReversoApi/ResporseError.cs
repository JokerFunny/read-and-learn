namespace Read_and_learn.Model.Translation.ReversoApi
{
    /// <summary>
    /// Base object for responces.
    /// </summary>
    public class ResporseError
    {
        /// <summary>
        /// Failer with error.
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Result successed.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }
    }
}
