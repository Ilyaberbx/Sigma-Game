using Odumbrata.Core;
using UnityEngine.AI;

namespace Odumbrata.Systems
{
    public abstract class BaseBrainSystem : BaseSystem
    {
        public abstract bool TryGetPath(NavMeshAgent agent, out NavMeshPath path);
    }
}