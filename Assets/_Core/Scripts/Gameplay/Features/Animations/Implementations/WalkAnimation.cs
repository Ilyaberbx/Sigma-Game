using Odumbrata.Features.Animations.Helpers;
using UnityEngine;

namespace Odumbrata.Features.Animations.Implementations
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