using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using DG.Tweening;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
using Odumbrata.Features.Animations;
using Odumbrata.Features.Animations.Implementations;
using Odumbrata.Features.Movement;
using Odumbrata.Features.Movement.Data;
using Odumbrata.Features.Movement.States;
using Odumbrata.Features.Stats;
using Odumbrata.Global.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player.States
{
    public class FacingData
    {
        public Vector3 StayAtPosition { get; }
        public Vector3 LookAtPosition { get; }
        public Transform Player { get; }

        public FacingData(Transform player, Vector3 stayAtPosition, Vector3 lookAtPosition)
        {
            StayAtPosition = stayAtPosition;
            LookAtPosition = lookAtPosition;
            Player = player;
        }
    }

    public class PlayerFaceAtState : BasePlayerState<FacingData>, IUpdatable
    {
        private UpdateService _updateService;

        private AnimationSystem _animationSystem;
        private MovementSystem _movementSystem;
        private StatsSystem _statsSystem;

        private readonly NavMeshAgent _agent;
        private TaskCompletionSource<bool> _completionSource;
        private CancellationToken _token;

        private Vector3 StayAtPosition => Data.StayAtPosition;
        private Vector3 LookAtPosition => Data.LookAtPosition;
        private Transform Player => Data.Player;


        public PlayerFaceAtState(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public override void Initialize(ISystemsContainer container, FacingData data)
        {
            base.Initialize(container, data);

            _animationSystem = container.GetSystem<AnimationSystem>();
            _movementSystem = container.GetSystem<MovementSystem>();
            _statsSystem = container.GetSystem<StatsSystem>();
            _updateService = ServiceLocator.Get<UpdateService>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _token = token;
            _completionSource = new TaskCompletionSource<bool>();
            _animationSystem.Set<IdleAnimation>();

            var canGo = NavMesh.SamplePosition(StayAtPosition, out var hit, Mathf.Infinity, NavMesh.AllAreas);

            if (!canGo)
            {
                return Task.CompletedTask;
            }

            var agent = Player.GetComponent<NavMeshAgent>();

            var sampledPosition = hit.position;

            var path = new NavMeshPath();

            if (agent.CalculatePath(sampledPosition, path))
            {
                Walk(agent, path);
            }
            else
            {
                _completionSource.SetResult(false);
            }


            _updateService.Add(this);

            return _completionSource.Task;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _updateService.Remove(this);

            return Task.CompletedTask;
        }

        public async void Tick(float deltaTime)
        {
            if (_agent.remainingDistance <= 0 && _movementSystem.CurrentMove.GetType() == typeof(WalkMove))
            {
                _movementSystem.Set<IdleMove>();
                var agent = _agent.transform;
                await agent.DOLookAt(LookAtPosition, 1, AxisConstraint.Y).AsTask(_token);
                _completionSource.SetResult(true);
            }
        }

        private void Walk(NavMeshAgent agent, NavMeshPath path)
        {
            if (!_statsSystem.TryGet<WalkStat>(out var walkStat))
            {
                return;
            }

            if (!_statsSystem.TryGet<AccelerationStat>(out var accelerationStat))
            {
                return;
            }

            if (!_statsSystem.TryGet<AngularSpeedStat>(out var angularSpeedStat))
            {
                return;
            }

            var walkData = new WalkData(agent, walkStat, accelerationStat, angularSpeedStat);

            walkData.SetPath(path);

            _movementSystem.Set<WalkMove, WalkData>(walkData);
            _animationSystem.Set<WalkAnimation>();
        }
    }
}