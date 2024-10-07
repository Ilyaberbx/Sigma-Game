using Odumbrata.Systems.Animations.Helpers;
using UnityEngine;

namespace Odumbrata.Systems.Animations.Implementations
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