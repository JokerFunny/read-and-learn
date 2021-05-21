using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Read_and_learn.Service
{
    /// <summary>
    /// Implementation of <see cref="IMessageBus"/>.
    /// </summary>
    public class MessageBus : IMessageBus
    {
        private readonly Dictionary<Type, List<Subscriber>> _handlers = new Dictionary<Type, List<Subscriber>>();

        public void Send<T>(T message)
        {
            var type = typeof(T);

            var handlers = _handlers.Where(o => o.Key == type).SelectMany(o => o.Value).ToList();

            foreach (var handler in handlers)
            {
                if (handler != null && handler.Delegate is Action<T> action)
                    action.Invoke(message);
            }
        }

        public void Subscribe<T>(Action<T> action, IEnumerable<string> tags = null)
        {
            Type messageType = typeof(T);

            var subscriber = new Subscriber
            {
                Delegate = action,
                Tags = tags
            };

            if (_handlers.ContainsKey(messageType))
                _handlers[messageType].Add(subscriber);
            else
                _handlers[messageType] = new List<Subscriber> { subscriber };
        }

        public void UnSubscribe(string tag)
        {
            foreach (var action in _handlers.Values)
                action.RemoveAll(o => o.Tags.Contains(tag));
        }

        private class Subscriber
        {
            private IEnumerable<string> _tags;

            /// <summary>
            /// Target action.
            /// </summary>
            public Delegate Delegate { get; set; }

            /// <summary>
            /// Target tags collection.
            /// </summary>
            public IEnumerable<string> Tags
            {
                get
                {
                    if (_tags == null) return new string[0];

                    return _tags;
                }
                set => _tags = value;
            }
        }
    }
}
