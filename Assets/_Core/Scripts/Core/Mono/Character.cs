using Better.Attributes.Runtime.Select;
using Odumbrata.Movement;
using Odumbrata.Movement.Brains;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Mono
{
    public class Character : Entity
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeReference, Select] private IBrain _brain;

        private MoveSystem _moveSystem;
        
        protected override void Initialize()
        {
            base.Initialize();

            _moveSystem = new MoveSystem(_brain, Stats, _agent);
        }

        //TODO: Tick Service
        private void Update()
        {
            _moveSystem.Tick();
        }
    }
}