using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Common.Door;
using Odumbrata.Behaviour.Player.States;
using Odumbrata.Services.Camera;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player
{
    public sealed class PlayerBehaviour : BaseStateBehaviour<BasePlayerState>, ICameraTarget, IDoorHandler
    {
        [SerializeField] private Transform _cameraFollowPoint;
        [SerializeField] private Transform _cameraLookAt;
        [SerializeField] private NavMeshAgent _agent;

        private PlayerMoveState _moveState;
        private PlayerIdleState _idleState;
        private bool _wasMoving;
        private bool _waitingForDoor;
        public Transform Follow => _cameraFollowPoint;
        public Transform LookAt => _cameraLookAt;

        protected override void Start()
        {
            base.Start();

            _moveState = new PlayerMoveState(_agent);
            _idleState = new PlayerIdleState(_agent);

            _moveState.OnReachDestination += OnDestinationReached;
            _idleState.OnValidPath += OnValidPath;

            SetupState(_moveState);
            SetupState(_idleState);

            SetState(_idleState).Forget();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _moveState.OnReachDestination -= OnDestinationReached;
            _idleState.OnValidPath -= OnValidPath;
        }

        private void OnValidPath(NavMeshPath path)
        {
            if (_waitingForDoor)
            {
                return;
            }

            SetState(_moveState).Forget();
        }

        private void OnDestinationReached()
        {
            SetState(_idleState).Forget();
        }

        public Task HandleDoorOpeningStarted(DoorInteractionData data)
        {
            _wasMoving = StateMachine.CurrentState == _moveState;
            _waitingForDoor = true;

            SetState(_idleState);

            return Task.CompletedTask;
        }

        public Task HandleDoorOpeningEnded(DoorInteractionData data)
        {
            if (_wasMoving)
            {
                _wasMoving = false;
                SetState(_moveState).Forget();
            }

            _waitingForDoor = false;

            return Task.CompletedTask;
        }
    }
}