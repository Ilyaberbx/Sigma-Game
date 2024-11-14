using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Utils;
using UnityEngine;

namespace Odumbrata.Behaviour.Common.Door
{
    public abstract class BaseDoorBehaviour : BaseBehaviour, IInteractable
    {
        [SerializeField] private Outline _outline;
        [SerializeField] private Transform _lookAt;
        [SerializeField] private Transform _root;
        [SerializeField] private int _secondsBeforeClosing;
        protected Transform Root => _root;
        public Outline Outline => _outline;

        public async Task Interact(InteractionData data)
        {
            if (IsOpened)
            {
                return;
            }

            var canNotHandle = !data.Player.TryGetComponent(out IDoorHandler handler);

            if (canNotHandle)
            {
                return;
            }

            var transitionPosition = GetInteractionPosition(handler);
            var lookAt = _lookAt.position;
            var handleData = new DoorHandleData(transitionPosition, lookAt);

            await handler.HandleDoorPreOpening(handleData);
            await Open();

            if (LeftOpenedAfterTransition)
            {
                return;
            }

            await TasksHelper.DelayInSeconds(_secondsBeforeClosing);
            await Close();
        }

        protected abstract bool LeftOpenedAfterTransition { get; }
        protected abstract bool IsOpened { get; }
        protected abstract Task Close();
        protected abstract Task Open();
        protected abstract Vector3 GetInteractionPosition(IDoorHandler handler);
    }
}