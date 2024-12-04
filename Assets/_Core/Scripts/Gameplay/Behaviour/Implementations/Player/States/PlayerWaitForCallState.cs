using System;
using System.Threading;
using System.Threading.Tasks;
using Odumbrata.Components.Animations;
using Odumbrata.Components.Animations.Implementations;
using Odumbrata.Components.Brains;
using Odumbrata.Components.InversionKinematics.Contexts;
using Odumbrata.Components.Movement;
using Odumbrata.Components.Movement.States;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
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

        protected override void Initialize()
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