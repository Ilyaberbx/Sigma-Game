using UnityEngine.AI;

namespace Odumbrata.Movement.Brains
{
    public interface IBrain
    {
        bool TryGetPath(NavMeshAgent agent, out NavMeshPath path);
    }
}