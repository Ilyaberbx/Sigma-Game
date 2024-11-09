using Odumbrata.Entity;
using Odumbrata.Features.InversionKinematics.Contexts;
using Odumbrata.Features.InversionKinematics.Profiles;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace Odumbrata.Behaviour
{
    public abstract class BaseHumanoidBehaviour<TBehaviourState> : BaseStateBehaviour<TBehaviourState>, IHumanoidContext
        where TBehaviourState : BaseEntityState
    {
        [Header("Humanoid Parameters")] [SerializeField]
        private NavMeshAgent _agent;

        [Header("Head")] [SerializeField] private Transform _lookAtPoint;
        [SerializeField] private Rig _lookAtRig;
        [Header("Hands")] [SerializeField] private Rig _leftHandGrabbingRig;
        [SerializeField] private Transform _leftHandGrabbingPoint;
        [SerializeField] private Rig _rightHandGrabbingRig;
        [SerializeField] private Transform _rightHandGrabbingPoint;

        public NavMeshAgent Agent => _agent;
        public Rig LookAtRig => _lookAtRig;
        public Rig LeftHandGrabbingRig => _leftHandGrabbingRig;
        public Rig RightHandGrabbingRig => _rightHandGrabbingRig;

        public void LookAt(Vector3 at)
        {
            _lookAtPoint.position = at;
        }

        public void Grab(HandType handType, Vector3 position)
        {
            var handPoint = handType == HandType.Left ? _leftHandGrabbingPoint : _rightHandGrabbingPoint;

            handPoint.position = position;
        }
    }
}