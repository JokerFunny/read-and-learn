using System;
using System.Collections.Generic;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface to handle message proceeding.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Send target <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="T">Type of message</typeparam>
        /// <param name="message">Target message</param>
        void Send<T>(T message);

        /// <summary>
        /// Subscribe target <typeparamref name="T"/> for target <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">Target message</typeparam>
        /// <param name="action">Target action to execute</param>
        /// <param name="tags">Additional tags</param>
        void Subscribe<T>(Action<T> action, IEnumerable<string> tags = null);

        /// <summary>
        /// Unsubscribe all subscribers with target tag.
        /// </summary>
        /// <param name="tag">Target tag</param>
        void UnSubscribe(string tag);
    }
}
