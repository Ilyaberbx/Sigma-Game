using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Player.States;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player
{
    public class PlayerBehaviour : BaseStateBehaviour<BasePlayerState>
    {
        [SerializeField] private NavMeshAgent _agent;
        private PlayerMoveState _moveState;
        private PlayerIdleState _idleState;

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