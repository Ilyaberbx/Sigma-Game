using Odumbrata.Components.Animations.Helpers;
using UnityEngine;

namespace Odumbrata.Components.Animations.Implementations
{
    public class WalkAnimation : BaseAnimation
    {
        public override string Key => AnimationKeys.IsMoving;

        public override void Apply(Animator animator)
        {
            animator.SetBool(Key, true);
        }
    }
}