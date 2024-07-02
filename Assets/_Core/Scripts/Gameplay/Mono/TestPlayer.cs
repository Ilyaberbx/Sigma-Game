using Better.Locators.Runtime;
using Odumbrata.Core;
using Odumbrata.Databases;
using Odumbrata.Gameplay.Systems;
using Odumbrata.Global.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Gameplay.Mono
{
    //TODO Remove
    public class TestPlayer : MonoBehaviour, IUpdatable
    {
        [SerializeField] private StatsDatabase _statsDatabase;
        [SerializeField] private NavMeshAgent _agent;

        private ISubscriptionHandler<IUpdatable> _updateSubscriptionHandler;

        private MoveSystem _moveSystem;

        private void Start()
        {
            _updateSubscriptionHandler = ServiceLocator.Get<UpdateService>();
            
            _moveSystem = new MoveSystem(new InputBrain(), new StatsSystem(_statsDatabase.Stats), _agent);

            _updateSubscriptionHandler.Subscribe(this); // TODO Game tick service
        }

        private void OnDestroy()
        {
            _updateSubscriptionHandler.Unsubscribe(this);
        }

        public void Tick(float deltaTime)
        {
            _moveSystem.Move();
        }
    }
}