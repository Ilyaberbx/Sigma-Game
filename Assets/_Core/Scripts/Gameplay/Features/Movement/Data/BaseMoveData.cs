using UnityEngine.AI;

namespace Odumbrata.Features.Movement.Data
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