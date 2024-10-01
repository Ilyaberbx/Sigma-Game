using System;
using Odumbrata.Core;
using UnityEngine;

namespace Odumbrata.Systems.Animations
{
    [Serializable]
    //TODO: Add pause handling
    public abstract class BaseAnimationSystem : BaseSystem
    {
        [SerializeField] private Animator _animator;

        protected Animator Animator => _animator;
    }
}