using System;
using UnityEngine;

namespace Odumbrata.Components.Animations
{
    [Serializable]
    public abstract class BaseAnimation
    {
        public abstract string Key { get; }
        public abstract void Apply(Animator animator);
    }
}