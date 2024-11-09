using System;
using System.Threading;
using System.Threading.Tasks;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
using Odumbrata.Features.Animations;
using Odumbrata.Features.Animations.Implementations;
using Odumbrata.Features.Brains;
using Odumbrata.Features.InversionKinematics.Contexts;
using Odumbrata.Features.Movement;
using Odumbrata.Features.Movement.States;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player.States
{
    public class PlayerWaitForCallState : BasePlayerState
    {
        public event Action<NavMeshPath> OnValidPath;

        private MovementSystem _movementSystem;
        private InputBrainSystem _inputBrainSystem;
        private AnimationSystem _animationSystem;

        private readonly IHumanoidContext _humanoidContext;

        public PlayerWaitForCallState(IHumanoidContext humanoidContext)
        {
            _humanoidContext = humanoidContext;
        }

        protected override void Initialize(ISystemsContainer container)
        {
            _inputBrainSystem = Container.GetSystem<InputBrainSystem>();
            _movementSystem = Container.GetSystem<MovementSystem>();
            _animationSystem = Container.GetSystem<AnimationSystem>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _movementSystem.Set<IdleMove, IdleData>(new IdleData(_humanoidContext.Agent));
            _animationSystem.Set<IdleAnimation>();

            _inputBrainSystem.OnPathValid += OnValidPathReceived;

            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _inputBrainSystem.OnPathValid -= OnValidPathReceived;

            return Task.CompletedTask;
        }


        private void OnValidPathReceived(NavMeshPath path)
        {
            OnValidPath.SafeInvoke(path);
        }
    }
}