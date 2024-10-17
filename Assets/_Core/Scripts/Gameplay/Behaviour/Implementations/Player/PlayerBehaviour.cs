using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Player.States;
using Odumbrata.Services.Camera;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Odumbrata.Behaviour.Player
{
    public sealed class PlayerBehaviour : BaseStateBehaviour<BasePlayerState>, ICameraTarget
    {
        [SerializeField] private Transform _cameraFollowPoint;
        [SerializeField] private Transform _cameraLookAt;
        [SerializeField] private NavMeshAgent _agent;

        private PlayerMoveState _moveState;
        private PlayerIdleState _idleState;
        public Transform Follow => _cameraFollowPoint;
        public Transform LookAt => _cameraLookAt;

        protected override void Start()
        {
            base.Start();

            _moveState = new PlayerMoveState(_agent);
            _idleState = new PlayerIdleState();

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
            SetState(_moveState).Forget();
        }

        private void OnDestinationReached()
        {
            SetState(_idleState).Forget();
        }
    }
}