using System;
using System.Collections.Generic;
using Better.Attributes.Runtime.Select;
using Odumbrata.Core;
using Odumbrata.Core.Container;
using UnityEngine;

namespace Odumbrata.Components.Animations
{
    [Serializable]
    public class AnimationSystem : BaseSystem
    {
        [SerializeField] private Animator _animator;

        [SerializeReference, Select(typeof(BaseAnimation))]
        private BaseAnimation[] _availableAnimations;

        private Dictionary<Type, BaseAnimation> _animationsMap = new();

        public override void Initialize(ISystemsContainerReadonly container)
        {
            base.Initialize(container);

            foreach (var animation in _availableAnimations)
            {
                _animationsMap.Add(animation.GetType(), animation);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _animationsMap.Clear();
        }

        public TAnimation Set<TAnimation>() where TAnimation : BaseAnimation, new()
        {
            if (TryGetAnimation<TAnimation>(out var animation))
            {
                animation.Apply(_animator);
                return (TAnimation)animation;
            }

            return null;
        }

        private bool TryGetAnimation<TAnimation>(out BaseAnimation animation) where TAnimation : BaseAnimation, new()
        {
            var type = typeof(TAnimation);

            var success = _animationsMap.TryGetValue(type, out animation);

            return success;
        }
    }
}