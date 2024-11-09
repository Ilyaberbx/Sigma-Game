using Odumbrata.Features.InversionKinematics.Profiles;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace Odumbrata.Features.InversionKinematics.Contexts
{
    public interface IHumanoidContext
    {
        public NavMeshAgent Agent { get; }
        public Rig LookAtRig { get; }
        public Rig LeftHandGrabbingRig { get; }
        public Rig RightHandGrabbingRig { get; }
        public void LookAt(Vector3 at);
        public void Grab(HandType handType, Vector3 position);
    }
}