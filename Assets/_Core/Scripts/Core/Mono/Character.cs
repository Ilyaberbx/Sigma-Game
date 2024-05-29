using System;
using Better.Attributes.Runtime.Select;
using Better.Locators.Runtime;
using Odumbrata.Movement;
using Odumbrata.Movement.Brains;
using Odumbrata.Services.Updates;
using Odumbrata.Tick;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Mono
{
    public class Character : Entity, IUpdatable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeReference, Select] private IBrain _brain;

        private MoveSystem _moveSystem;
        private UpdateService _updateService;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _moveSystem = new MoveSystem(_brain, Stats, _agent);
            
            _updateService = ServiceLocator.Get<UpdateService>();
        }

        private void Start()
        {
            _updateService.Subscribe(this);
        }

        private void OnDestroy()
        {
            _updateService.Unsubscribe(this);
        }
        
        public void Tick(float deltaTime)
        {
            _moveSystem.Move();
        }
    }
}