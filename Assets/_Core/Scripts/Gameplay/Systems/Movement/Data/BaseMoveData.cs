using UnityEngine.AI;

namespace Odumbrata.Systems.Movement.Data
{
    public abstract class BaseMoveData
    {
        public NavMeshAgent Agent { get; }

        protected BaseMoveData(NavMeshAgent agent)
        {
            Agent = agent;
        }
    }
}