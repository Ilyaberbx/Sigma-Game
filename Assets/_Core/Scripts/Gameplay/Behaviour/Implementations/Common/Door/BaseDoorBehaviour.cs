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

        [SerializeField] private Transform _root;
        [SerializeField] private DoorHandlerTriggerObserver _handlerObserver;
        [SerializeField] private float _delayBeforeClosing;

        public float DelayBeforeClosing => _delayBeforeClosing;
        public abstract bool LeftOpenedAfterTransition { get; }
        public abstract bool IsOpened { get; }
        public abstract bool InProgress { get; }
        public abstract Task Close();
        public abstract Task Open();
        public abstract Vector3 GetInteractionPosition(Side side);
        protected Transform Root => _root;

        private void Start()
        {
            _handlerObserver.OnTriggerEntered += OnHandlerEntered;
        }

        private void OnDestroy()
        {
            _handlerObserver.OnTriggerEntered -= OnHandlerEntered;
        }

        private void OnHandlerEntered(IDoorHandler handler)
        {
            if (InProgress)
            {
                return;
            }

            OnHandlerEnter.SafeInvoke(handler, this);
        }
    }
}