using System;
using Odumbrata.Systems.Movement.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Systems.Movement.States
{
    [Serializable]
    public class WalkState : BaseMoveState<WalkData>
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
        }
    }
}