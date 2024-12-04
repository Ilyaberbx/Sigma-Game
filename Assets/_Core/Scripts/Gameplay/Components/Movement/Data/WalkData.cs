using Odumbrata.Components.Stats;
using UnityEngine.AI;

namespace Odumbrata.Components.Movement.Data
{
    public class WalkData : BaseMoveData
    {
        public AccelerationStat AccelerationStat { get; }
        public AngularSpeedStat AngularSpeedStat { get; }
        public WalkStat WalkStat { get; }
        public NavMeshPath Path { get; private set; }

        public WalkData(NavMeshAgent agent,
            WalkStat walkStat,
            AccelerationStat accelerationStat,
            AngularSpeedStat angularSpeedStat) : base(agent)
        {
            WalkStat = walkStat;
            AccelerationStat = accelerationStat;
            AngularSpeedStat = angularSpeedStat;
        }

        public void SetPath(NavMeshPath path)
        {
            Path = path;
        }
    }
}