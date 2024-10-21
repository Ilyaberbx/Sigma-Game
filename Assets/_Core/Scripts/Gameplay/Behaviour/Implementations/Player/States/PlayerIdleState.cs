using System;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
using Odumbrata.Features.Animations;
using Odumbrata.Features.Animations.Implementations;
using Odumbrata.Features.Brains;
using Odumbrata.Features.Movement;
using Odumbrata.Features.Movement.States;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player.States
{
    public class PlayerIdleState : BasePlayerState
    {
        private readonly NavMeshAgent _agent;
        public event Action<NavMeshPath> OnValidPath;

        private MovementSystem _movementSystem;
        private InputBrainSystem _inputBrainSystem;
        private AnimationSystem _animationSystem;

        public PlayerIdleState(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public override void Initialize(ISystemsContainer container)
        {
            base.Initialize(container);

            _inputBrainSystem = Container.GetSystem<InputBrainSystem>();
            _movementSystem = Container.GetSystem<MovementSystem>();
            _animationSystem = Container.GetSystem<AnimationSystem>();
        }

        public override void OnEntered()
        {
            _movementSystem.Set<IdleState, IdleData>(new IdleData(_agent));
            _animationSystem.Set<IdleAnimation>();

            _inputBrainSystem.OnPathValid += OnValidPathReceived;
        }

        public override void OnExited()
        {
            _inputBrainSystem.OnPathValid -= OnValidPathReceived;
        }

        private void OnValidPathReceived(NavMeshPath path)
        {
            OnValidPath.SafeInvoke(path);
        }
    }
}