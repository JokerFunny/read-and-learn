namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicate when page go to fullscreen mode.
    /// </summary>
    public class FullscreenRequestMessage
    {
        bool _pageFullscreen;

        /// <summary>
        /// Is fullscreen allowed.
        /// </summary>
        public bool Fullscreen
            => _pageFullscreen && UserSettings.Reader.Fullscreen;

        /// <summary>
        /// Specify if current context allows fullscreen mode.
        /// </summary>
        /// <param name="setFullscreen">Target value</param>
        public FullscreenRequestMessage(bool setFullscreen)
            => _pageFullscreen = setFullscreen;
    }
}
