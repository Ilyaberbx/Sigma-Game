using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using DG.Tweening;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
using Odumbrata.Features.Animations;
using Odumbrata.Features.Animations.Implementations;
using Odumbrata.Features.InversionKinematics;
using Odumbrata.Features.InversionKinematics.Contexts;
using Odumbrata.Features.InversionKinematics.Profiles;
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
        public float Duration { get; }

        public FacingData(Transform player, Vector3 stayAtPosition, Vector3 lookAtPosition, float duration)
        {
            StayAtPosition = stayAtPosition;
            LookAtPosition = lookAtPosition;
            Duration = duration;
            Player = player;
        }
    }

    public class PlayerFaceAtState : BasePlayerState<FacingData>, IUpdatable
    {
        private UpdateService _updateService;

        private AnimationSystem _animationSystem;
        private MovementSystem _movementSystem;
        private StatsSystem _statsSystem;
        private IkSystem _ikSystem;

        private TaskCompletionSource<bool> _completionSource;
        private CancellationToken _token;
        private readonly IHumanoidContext _humanoidContext;

        private Vector3 StayAtPosition => Data.StayAtPosition;
        private Vector3 LookAtPosition => Data.LookAtPosition;
        private Transform Player => Data.Player;

        public PlayerFaceAtState(IHumanoidContext humanoidContext)
        {
            _humanoidContext = humanoidContext;
        }

        public override void Initialize(ISystemsContainer container, FacingData data)
        {
            base.Initialize(container, data);

            _animationSystem = container.GetSystem<AnimationSystem>();
            _movementSystem = container.GetSystem<MovementSystem>();
            _statsSystem = container.GetSystem<StatsSystem>();
            _ikSystem = container.GetSystem<IkSystem>();
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
                _completionSource.SetResult(false);

                return Task.CompletedTask;
            }

            var agent = Player.GetComponent<NavMeshAgent>();
            var sampledPosition = hit.position;
            var path = new NavMeshPath();

            if (agent.CalculatePath(sampledPosition, path))
            {
                Walk(agent, path);
            }

            _updateService.Add(this);

            return _completionSource.Task;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _updateService.Remove(this);

            _ikSystem.ClearImmediately();

            return Task.CompletedTask;
        }

        public async void Tick(float deltaTime)
        {
            var agent = _humanoidContext.Agent;
            var isMoving = _movementSystem.CurrentMove.GetType() == typeof(WalkMove);

            if (agent.remainingDistance <= 0 && isMoving)
            {
                _movementSystem.Set<IdleMove>();
                _animationSystem.Set<IdleAnimation>();

                var halfDuration = Data.Duration / 2f;

                await agent.transform.DOLookAt(LookAtPosition, halfDuration, AxisConstraint.Y).AsTask(_token);
                await ApplyIkProfiles();
                await _ikSystem.Clear(halfDuration);

                _completionSource.SetResult(true);
            }
        }

        private async Task ApplyIkProfiles()
        {
            _ikSystem.ClearImmediately();

            var faceAtProfile = new FaceAtProfile();
            var grabProfile = new GrabProfile();

            faceAtProfile.Initialize(_humanoidContext, Data.LookAtPosition);
            grabProfile.Initialize(_humanoidContext, new GrabData(HandType.Right, Data.LookAtPosition));

            var halfDuration = Data.Duration / 2f;

            var ikData = new IkTransitionData(halfDuration, faceAtProfile, grabProfile);

            await _ikSystem.ProcessTransition(ikData);
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