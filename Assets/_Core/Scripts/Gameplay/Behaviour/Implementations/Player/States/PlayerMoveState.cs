using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
using Odumbrata.Features.Animations;
using Odumbrata.Features.Animations.Implementations;
using Odumbrata.Features.Brains;
using Odumbrata.Features.InversionKinematics.Contexts;
using Odumbrata.Features.Movement;
using Odumbrata.Features.Movement.Data;
using Odumbrata.Features.Movement.States;
using Odumbrata.Features.Stats;
using Odumbrata.Global.Services;
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

        private readonly IHumanoidContext _humanoidContext;
        private WalkData _data;

        private NavMeshPath Path => _inputBrainSystem.Path;

        public PlayerMoveState(IHumanoidContext humanoidContext)
        {
            _humanoidContext = humanoidContext;
        }

        protected override void Initialize(ISystemsContainer container)
        {
            _movementSystem = Container.GetSystem<MovementSystem>();
            _statsSystem = Container.GetSystem<StatsSystem>();
            _inputBrainSystem = Container.GetSystem<InputBrainSystem>();
            _animationSystem = Container.GetSystem<AnimationSystem>();

            _updateService = ServiceLocator.Get<UpdateService>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _inputBrainSystem.OnPathValid += OnValidPathReceived;

            Move(Path);

            _updateService.Add(this);

            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            SetPath(null);
            _updateService.Remove(this);
            _inputBrainSystem.OnPathValid -= OnValidPathReceived;

            return Task.CompletedTask;
        }

        public void Tick(float deltaTime)
        {
            if (_data.Path != null && _humanoidContext.Agent.remainingDistance <= 0)
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
            _movementSystem.Set<WalkMove, WalkData>(_data);
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

            data = new WalkData(_humanoidContext.Agent, walkStat, accelerationStat, angularSpeedStat);

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