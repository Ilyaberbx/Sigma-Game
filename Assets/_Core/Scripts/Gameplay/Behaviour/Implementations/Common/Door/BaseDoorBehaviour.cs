using System;
using System.Threading.Tasks;
using Odumbrata.Behaviour.Common.Observers;
using Odumbrata.Extensions;
using UnityEngine;

namespace Odumbrata.Behaviour.Common.Door
{
    public abstract class BaseDoorBehaviour : BaseBehaviour
    {
        public event Action<IDoorHandler, BaseDoorBehaviour> OnHandlerEnter;

        [SerializeField] private Transform _lookAt;
        [SerializeField] private Transform _root;
        [SerializeField] private DoorHandlerTriggerObserver[] _observers;
        [SerializeField] private float _delayBeforeClosing;

        public float DelayBeforeClosing => _delayBeforeClosing;
        public Transform LookAtPoint => _lookAt;
        protected Transform Root => _root;

        private void Start()
        {
            foreach (var observer in _observers)
            {
                observer.OnTriggerEntered += OnHandlerEntered;
            }
        }

        private void OnDestroy()
        {
            foreach (var observer in _observers)
            {
                observer.OnTriggerEntered -= OnHandlerEntered;
            }
        }

        public void SetActiveObservers(bool value)
        {
            foreach (var observer in _observers)
            {
                observer.SetActive(value);
            }
        }

        private void OnHandlerEntered(IDoorHandler handler)
        {
            OnHandlerEnter.SafeInvoke(handler, this);
        }

        public abstract bool LeftOpenedAfterTransition { get; }
        public abstract bool IsOpened { get; }
        public abstract Task Close();
        public abstract Task Open();
        public abstract Vector3 GetInteractionPosition(IDoorHandler handler);
    }
}