using System;
using Better.Locators.Runtime;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
using Odumbrata.Global.Services;
using Odumbrata.Systems.Animations;
using Odumbrata.Systems.Animations.Implementations;
using Odumbrata.Systems.Brains;
using Odumbrata.Systems.Movement;
using Odumbrata.Systems.Movement.Data;
using Odumbrata.Systems.Movement.States;
using Odumbrata.Systems.Stats;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player.States
{
    public class PlayerMoveState : BasePlayerState, IUpdatable
    {
        public event Action OnReachDestination;

        private UpdateService _updateService;

        private MovementSystem _movementSystem;
        private AnimationSystem _animationSystem;
        private InputBrainSystem _inputBrainSystem;
        private StatsSystem _statsSystem;

        private readonly NavMeshAgent _agent;

        private WalkData _data;

        private NavMeshPath Path => _inputBrainSystem.Path;

        public PlayerMoveState(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public override void Initialize(ISystemsContainer container)
        {
            base.Initialize(container);

            _movementSystem = Container.GetSystem<MovementSystem>();
            _statsSystem = Container.GetSystem<StatsSystem>();
            _inputBrainSystem = Container.GetSystem<InputBrainSystem>();
            _animationSystem = Container.GetSystem<AnimationSystem>();

            _updateService = ServiceLocator.Get<UpdateService>();
        }

        public override void OnEntered()
        {
            _updateService.Add(this);
            _inputBrainSystem.OnPathValid += OnValidPathReceived;

            Move(Path);
        }

        public override void OnExited()
        {
            SetPath(null);
            _updateService.Remove(this);
            _inputBrainSystem.OnPathValid -= OnValidPathReceived;
        }

        public void Tick(float deltaTime)
        {
            if (_data.Path != null && _agent.remainingDistance <= 0)
            {
                SetPath(null);
                OnReachDestination.SafeInvoke();
            }
        }

        private void OnValidPathReceived(NavMeshPath path)
        {
            Move(path);
        }

        private void SetPath(NavMeshPath path)
        {
            CreateDataIfNull();

            _data.SetPath(path);
        }

        private void Move(NavMeshPath path)
        {
            SetPath(path);
            _movementSystem.Set<WalkState, WalkData>(_data);
            _animationSystem.Set<WalkAnimation>();
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

        private void CreateDataIfNull()
        {
            if (_data == null)
            {
                TryGetWalkData(out _data);
            }
        }
    }
}