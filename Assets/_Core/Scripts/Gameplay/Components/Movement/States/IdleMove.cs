using System;
using Odumbrata.Components.Movement.Data;
using UnityEngine.AI;

namespace Odumbrata.Components.Movement.States
{
    [Serializable]
    public class IdleMove : BaseMove<IdleData>
    {
        private bool _wasStopped;

        public override void OnEntered()
        {
            _wasStopped = Data.Agent.isStopped;
            Data.Agent.isStopped = true;
        }

        public override void OnExited()
        {
            Data.Agent.isStopped = _wasStopped;
            _wasStopped = false;
        }
    }

    public class IdleData : BaseMoveData
    {
        public IdleData(NavMeshAgent agent) : base(agent)
        {
        }
    }
}