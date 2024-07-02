using UnityEngine.AI;

namespace Odumbrata.Gameplay.Systems
{
    public interface IBrain
    {
        bool TryGetPath(NavMeshAgent agent, out NavMeshPath path);
    }
}