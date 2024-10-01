using Better.Locators.Runtime;
using Odumbrata.Global.Services;
using Odumbrata.Systems;
using Odumbrata.Systems.Movement;
using Odumbrata.Systems.Movement.Data;
using Odumbrata.Systems.Movement.States;
using Odumbrata.Systems.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player.States
{
    public class PlayerMoveState : BasePlayerState, IUpdatable
    {
        private UpdateService _updateService;

        private MovementSystem _movementSystem;
        private InputBrainSystem _inputBrainSystem;
        private StatsSystem _statsSystem;

        private WalkData _data;
        private readonly NavMeshAgent _agent;

        public PlayerMoveState(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public override void OnEntered()
        {
            Debug.Log("Player Move State entered");

            _movementSystem = Container.GetSystem<MovementSystem>();
            _statsSystem = Container.GetSystem<StatsSystem>();
            _inputBrainSystem = Container.GetSystem<InputBrainSystem>();

            _updateService = ServiceLocator.Get<UpdateService>();

            _updateService.Add(this);

            if (TryGetWalkData(out var data))
            {
                _data = data;
            }
        }

        public override void OnExited()
        {
            Debug.Log("Player Move State exited");

            _updateService.Remove(this);
        }

        public void Tick(float deltaTime)
        {
            if (_data == null)
            {
                return;
            }

            if (_inputBrainSystem.TryGetPath(_agent, out var path))
            {
                _data.SetPath(path);
                _movementSystem.SetMove<WalkState, WalkData>(_data);
            }
        }

        private bool TryGetWalkData(out WalkData data)
        {
            data = null;

            if (!_statsSystem.TryGet<WalkStat>(out var walkStat))
            {
                return false;
            }

            if (!_statsSystem.TryGet<AccelerationStat>(out var accelerationStat))
            {
                return false;
            }

            if (!_statsSystem.TryGet<AngularSpeedStat>(out var angularSpeedStat))
            {
                return false;
            }

            data = new WalkData(_agent, walkStat, accelerationStat, angularSpeedStat);

            return true;
        }
    }
}