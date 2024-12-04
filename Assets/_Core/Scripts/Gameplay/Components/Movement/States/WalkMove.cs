using System;
using Odumbrata.Components.Movement.Data;
using UnityEngine.AI;

namespace Odumbrata.Components.Movement.States
{
    [Serializable]
    public class WalkMove : BaseMove<WalkData>
    {
        private NavMeshAgent Agent => Data.Agent;

        public override void OnEntered()
        {
            Agent.isStopped = false;
            Agent.angularSpeed = Data.AngularSpeedStat.Value;
            Agent.acceleration = Data.AccelerationStat.Value;
            Agent.speed = Data.WalkStat.Value;

            Agent.SetPath(Data.Path);
        }

        public override void OnExited()
        {
            Agent.angularSpeed = 0f;
            Agent.acceleration = 0f;
            Agent.speed = 0f;
            Agent.isStopped = true;

            Agent.SetDestination(Agent.transform.position);
        }
    }
}