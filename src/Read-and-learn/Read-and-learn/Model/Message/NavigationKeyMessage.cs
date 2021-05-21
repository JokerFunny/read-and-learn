namespace Read_and_learn.Model.Message
{
    /// <summary>
    /// Indicates navigation key action.
    /// </summary>
    public class NavigationKeyMessage
    {
        /// <summary>
        /// Target navigation key.
        /// </summary>
        public Key? Key { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="key">Target key</param>
        private NavigationKeyMessage(Key? key)
        {
            Key = key;
        }

        /// <summary>
        /// Get instance of current message from <paramref name="keyCode"/>.
        /// </summary>
        /// <param name="keyCode">Target navigation key code</param>
        /// <returns>
        ///     Instance of <see cref="NavigationKeyMessage"/> with target key.
        /// </returns>
        public static NavigationKeyMessage FromKeyCode(int keyCode)
        {
            Key? key = null;

            switch (keyCode)
            {
                case 32:
                    key = Message.Key.Space;
                    break;
                case 37:
                    key = Message.Key.ArrowLeft;
                    break;
                case 39:
                    key = Message.Key.ArrowRight;
                    break;
                case 38:
                    key = Message.Key.ArrowUp;
                    break;
                case 40:
                    key = Message.Key.ArrowDown;
                    break;
            }

            return new NavigationKeyMessage(key);
        }
    }

    /// <summary>
    /// Enum for supported navigation buttons.
    /// </summary>
    public enum Key
    {
        Space,
        ArrowLeft,
        ArrowRight,
        ArrowUp,
        ArrowDown,
    }
}
