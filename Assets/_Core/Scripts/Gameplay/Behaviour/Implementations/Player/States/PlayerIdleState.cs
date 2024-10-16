using System;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
using Odumbrata.Systems.Animations;
using Odumbrata.Systems.Animations.Implementations;
using Odumbrata.Systems.Brains;
using Odumbrata.Systems.Movement;
using Odumbrata.Systems.Movement.States;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player.States
{
    public class PlayerIdleState : BasePlayerState
    {
        public event Action<NavMeshPath> OnValidPath;

        private MovementSystem _movementSystem;
        private InputBrainSystem _inputBrainSystem;
        private AnimationSystem _animationSystem;

        public override void Initialize(ISystemsContainer container)
        {
            base.Initialize(container);

            _inputBrainSystem = Container.GetSystem<InputBrainSystem>();
            _movementSystem = Container.GetSystem<MovementSystem>();
            _animationSystem = Container.GetSystem<AnimationSystem>();
        }

        public override void OnEntered()
        {
            _movementSystem.Set<IdleState>();
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