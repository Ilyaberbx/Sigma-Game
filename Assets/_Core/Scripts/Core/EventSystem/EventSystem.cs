using System;
using System.Collections.Generic;
using UnityEngine;

namespace Odumbrata.Core.EventSystem
{
    public sealed class EventSystem : IDisposable
    {
        private readonly Dictionary<Type, List<Delegate>> _eventHandlers = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<T>(Action<object, T> handler) where T : IEventArg
        {
            var eventType = typeof(T);

            if (!_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType] = new List<Delegate>();
            }

            _eventHandlers[eventType].Add(handler);
        }

        public void Unsubscribe<T>(Action<object, T> handler) where T : IEventArg
        {
            var eventType = typeof(T);

            if (_eventHandlers.TryGetValue(eventType, out var eventHandler))
            {
                eventHandler.Remove(handler);
            }
        }

        public void Publish<T>(object sender, T eventArgs) where T : IEventArg
        {
            var eventType = typeof(T);

            Debug.Log("Publishing: " + eventType.Name);

            if (_eventHandlers.TryGetValue(eventType, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    ((Action<object, T>)handler)?.Invoke(sender, eventArgs);
                }
            }
        }

        public void Dispose()
        {
            _eventHandlers.Clear();
        }
    }

    public interface IEventArg
    {
    }
}