using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Player.States;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player
{
    public class PlayerBehaviour : BaseStateBehaviour<BasePlayerState>
    {
        [SerializeField] private NavMeshAgent _agent;

        protected override void Start()
        {
            base.Start();

            SetState(new PlayerMoveState(_agent)).Forget();
        }
    }
}