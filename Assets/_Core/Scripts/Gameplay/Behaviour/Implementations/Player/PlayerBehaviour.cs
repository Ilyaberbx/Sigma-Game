using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Common.Door;
using Odumbrata.Behaviour.Player.States;
using Odumbrata.Entity;
using Odumbrata.Services.Camera;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player
{
    public sealed class PlayerBehaviour : BaseStateBehaviour<BaseEntityState>, ICameraTarget, IDoorHandler
    {
        [SerializeField] private Transform _cameraFollowPoint;
        [SerializeField] private Transform _cameraLookAt;
        [SerializeField] private NavMeshAgent _agent;

        private PlayerMoveState _moveState;
        private PlayerWaitForCallState _waitForCallState;
        private bool _wasMoving;
        private bool _waitingForDoor;
        public Transform Follow => _cameraFollowPoint;
        public Transform LookAt => _cameraLookAt;

        protected override void Start()
        {
            base.Start();

            _moveState = new PlayerMoveState(_agent);
            _waitForCallState = new PlayerWaitForCallState(_agent);

            _moveState.OnReachDestination += OnDestinationReached;
            _waitForCallState.OnValidPath += OnValidPath;

            SetStateAsync(_waitForCallState).Forget();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _moveState.OnReachDestination -= OnDestinationReached;
            _waitForCallState.OnValidPath -= OnValidPath;
        }

        private void OnValidPath(NavMeshPath path)
        {
            if (_waitingForDoor)
            {
                return;
            }

            SetStateAsync(_moveState).Forget();
        }

        private void OnDestinationReached()
        {
            SetStateAsync(_waitForCallState).Forget();
        }

        public async Task HandleDoorPreOpening(DoorTransitionData data)
        {
            _wasMoving = StateMachine.CurrentState == _moveState;
            _waitingForDoor = true;

            var facingData = new FacingData(Transform, data.InteractionPosition, data.LookAtPosition);
            var faceAtState = new PlayerFaceAtState(_agent);

            await SetStateAsync(faceAtState, facingData);
            await SetStateAsync(_waitForCallState);
        }

        public Task HandleDoorPostOpening(DoorTransitionData data)
        {
            if (_wasMoving)
            {
                _wasMoving = false;
                SetStateAsync(_moveState).Forget();
            }

            _waitingForDoor = false;

            return Task.CompletedTask;
        }
    }
}