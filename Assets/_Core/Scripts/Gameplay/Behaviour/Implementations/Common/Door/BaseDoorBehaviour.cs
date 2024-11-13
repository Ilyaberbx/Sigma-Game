using System;
using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Extensions;
using UnityEngine;

namespace Odumbrata.Behaviour.Common.Door
{
    public abstract class BaseDoorBehaviour : BaseBehaviour, IInteractable
    {
        public event Action<IDoorHandler, BaseDoorBehaviour> OnInteraction;

        [SerializeField] private Outline _outline;
        [SerializeField] private Transform _lookAt;
        [SerializeField] private Transform _root;
        [SerializeField] private float _delayBeforeClosing;

        private TaskCompletionSource<bool> _interactionSource;
        public float DelayBeforeClosing => _delayBeforeClosing;
        public Transform LookAtPoint => _lookAt;
        protected Transform Root => _root;
        public Outline Outline => _outline;

        public Task Interact(InteractionData data)
        {
            var player = data.Player;

            _interactionSource = new TaskCompletionSource<bool>();

            OnInteraction.SafeInvoke(player, this);

            return _interactionSource.Task;
        }

        public void FireFinishInteraction()
        {
            _interactionSource?.SetResult(true);
        }

        public abstract bool LeftOpenedAfterTransition { get; }
        public abstract bool IsOpened { get; }
        public abstract Task Close();
        public abstract Task Open();
        public abstract Vector3 GetInteractionPosition(IDoorHandler handler);
    }
}