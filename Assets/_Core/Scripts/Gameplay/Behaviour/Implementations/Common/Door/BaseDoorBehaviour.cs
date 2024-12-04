using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Core.EventSystem;
using Odumbrata.Data.Runtime;
using UnityEngine;

namespace Odumbrata.Behaviour.Common.Door
{
    #region Events

    public struct DoorOpenedArg : IEventArg
    {
        public BaseDoorBehaviour DoorBehaviour { get; }
        public bool IsFront { get; }

        public DoorOpenedArg(BaseDoorBehaviour doorBehaviour, bool isFront)
        {
            DoorBehaviour = doorBehaviour;
            IsFront = isFront;
        }
    }

    public struct DoorPreOpenArg : IEventArg
    {
        public BaseDoorBehaviour DoorBehaviour { get; }
        public bool IsFront { get; }

        public DoorPreOpenArg(BaseDoorBehaviour doorBehaviour, bool isFront)
        {
            DoorBehaviour = doorBehaviour;
            IsFront = isFront;
        }
    }

    public struct DoorClosedArg : IEventArg
    {
        public BaseDoorBehaviour DoorBehaviour { get; }

        public bool IsFront { get; }

        public DoorClosedArg(BaseDoorBehaviour doorBehaviour, bool isFront)
        {
            DoorBehaviour = doorBehaviour;
            IsFront = isFront;
        }
    }

    #endregion

    public abstract class BaseDoorBehaviour : BaseBehaviour, IInteractable
    {
        private const int ToMillisecondsEquivalent = 1000;

        [SerializeField] private Outline _outline;
        [SerializeField] private Transform _lookAt;
        [SerializeField] private Transform _root;
        [SerializeField] private float _delayBeforeClosing;
        private EventSystem _events;
        private DoorRuntimeData _runtimeData;

        private bool IsOpened => _runtimeData.IsOpened;
        protected Transform Root => _root;
        public Outline Outline => _outline;

        public void Initialize(DoorRuntimeData data, EventSystem events)
        {
            _runtimeData = data;
            _events = events;

            if (_runtimeData.IsLocked)
            {
                CloseImmediately();
                return;
            }

            if (_runtimeData.IsOpened)
            {
                OpenImmediately();
            }
            else
            {
                CloseImmediately();
            }
        }

        public async Task Interact(InteractionData data)
        {
            if (IsOpened)
            {
                return;
            }

            var player = data.Player;
            var isFront = IsFront(player);
            var interactionPosition = isFront ? FrontInteractionPosition : BackInteractionPosition;
            var lookAt = _lookAt.position;
            var handleData = new DoorHandleData(interactionPosition, lookAt);

            _events.Publish(this, new DoorPreOpenArg(this, isFront));
            await player.HandleDoorPreOpening(handleData);
            await Open();
            _runtimeData.IsOpened = true;
            _events.Publish(this, new DoorOpenedArg(this, isFront));

            if (LeftOpenedAfterTransition)
            {
                return;
            }

            var delay = (int)_delayBeforeClosing * ToMillisecondsEquivalent;
            await Task.Delay(delay);
            await Close();
            _runtimeData.IsOpened = false;
            _events.Publish(this, new DoorClosedArg(this, isFront));
        }

        private bool IsFront(IDoorHandler handler)
        {
            var handlerPosition = handler.Position;
            var frontDistance = Vector3.Distance(handlerPosition, FrontInteractionPosition);
            var backDistance = Vector3.Distance(handlerPosition, BackInteractionPosition);
            return frontDistance < backDistance;
        }

        protected abstract bool LeftOpenedAfterTransition { get; }
        protected abstract Vector3 FrontInteractionPosition { get; }
        protected abstract Vector3 BackInteractionPosition { get; }
        protected abstract Task Close();
        protected abstract Task Open();
        protected abstract void CloseImmediately();
        protected abstract void OpenImmediately();
    }
}