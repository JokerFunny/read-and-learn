namespace Read_and_learn.PlatformRelatedServices
{
    /// <summary>
    /// Interface for platform-specific realization of toast-notifications (quick little messages).
    /// </summary>
    public interface IToastService
    {
        /// <summary>
        /// Show quick little messsage with target <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Target message to show</param>
        void Show(string message);
    }
}
