using Odumbrata.Movement.Brains;
using Odumbrata.Stats;
using Odumbrata.Stats.Implementations;
using UnityEngine.AI;

namespace Odumbrata.Movement
{
    public class MoveSystem : ISystem
    {
        private readonly IBrain _brain;
        private readonly StatsSystem _stats;
        private readonly NavMeshAgent _agent;

        public MoveSystem(IBrain brain, StatsSystem stats, NavMeshAgent agent)
        {
            _brain = brain;
            _stats = stats;
            _agent = agent;
        }

        public void Tick(float deltaTime)
        {
            if (_stats.TryGet<WalkSpeedStat>(out var walk))
            {
                _agent.speed = walk.Value;
            }
            
            if (_stats.TryGet<AngularSpeedStat>(out var angular))
            {
                _agent.angularSpeed = angular.Value;
            }
            
            if (_stats.TryGet<AccelerationStat>(out var acceleration))
            {
                _agent.acceleration = acceleration.Value;
            }

            if (_brain.TryGetPath(_agent, out NavMeshPath path))
            {
                _agent.SetPath(path);
            }
        }
    }
}