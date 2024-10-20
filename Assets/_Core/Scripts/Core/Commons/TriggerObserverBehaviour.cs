using System;
using Odumbrata.Behaviour;
using Odumbrata.Extensions;
using UnityEngine;

namespace Odumbrata.Core.Commons
{
    public class TriggerObserverBehaviour<TComponent> : BaseBehaviour
    {
        public event Action<TComponent> OnTriggerEntered;
        public event Action<TComponent> OnTriggerExited;
        public event Action<TComponent> OnTriggerStaying;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<TComponent>(out var component))
            {
                OnTriggerEntered.SafeInvoke(component);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<TComponent>(out var component))
            {
                OnTriggerStaying.SafeInvoke(component);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<TComponent>(out var component))
            {
                OnTriggerExited.SafeInvoke(component);
            }
        }
    }
}