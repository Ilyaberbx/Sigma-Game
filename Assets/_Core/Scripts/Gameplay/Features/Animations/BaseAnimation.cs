using System;
using UnityEngine;

namespace Odumbrata.Features.Animations
{
    [Serializable]
    public abstract class BaseAnimation
    {
        public abstract string Key { get; }
        public abstract void Apply(Animator animator);
    }
}