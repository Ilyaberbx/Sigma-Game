using System;
using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Data.Runtime;
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

        public bool IsOpened => RuntimeData.IsOpen;
        public float DelayBeforeClosing => _delayBeforeClosing;
        public Transform LookAtPoint => _lookAt;
        protected Transform Root => _root;
        public Outline Outline => _outline;
        protected DoorRuntimeData RuntimeData { get; private set; }

        public void SetData(DoorRuntimeData data)
        {
            RuntimeData = data;

            //TODO: Finalize
            // if (RuntimeData.IsLocked)
            // {
            //     CloseImmediately();
            //     return;
            // }
            //
            // if (RuntimeData.IsOpen)
            // {
            //     OpenImmediately();
            // }
            // else
            // {
            //     CloseImmediately();
            // }
        }

        public Task Interact(InteractionData data)
        {
            var player = data.Player;

            _interactionSource = new TaskCompletionSource<bool>();

            OnInteraction.SafeInvoke(player, this);

            return _interactionSource.Task;
        }

        private void OnDestroy()
        {
            _interactionSource?.SetResult(true);
        }

        public void FinishInteraction()
        {
            _interactionSource?.SetResult(true);
        }

        public abstract bool LeftOpenedAfterTransition { get; }
        public abstract Vector3 FrontInteractionPosition { get; }
        public abstract Vector3 BackInteractionPosition { get; }
        public abstract Task Close();
        public abstract Task Open();
        public abstract void CloseImmediately();
        public abstract void OpenImmediately();
    }
}