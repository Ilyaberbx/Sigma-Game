using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Odumbrata.Features.InversionKinematics.Profiles
{
    public enum HandType
    {
        Left,
        Right
    }

    public class GrabData
    {
        public GrabData(HandType handType, Vector3 position)
        {
            HandType = handType;
            Position = position;
        }

        public HandType HandType { get; }
        public Vector3 Position { get; }
    }

    public class GrabProfile : BaseIkProfile<GrabData>
    {
        public override void Apply(RigBuilder builder)
        {
            HumanoidContext.Grab(Data.HandType, Data.Position);

            var rig = Data.HandType == HandType.Left
                ? HumanoidContext.LeftHandGrabbingRig
                : HumanoidContext.RightHandGrabbingRig;

            rig.weight = 0;

            var layer = new RigLayer(rig);
            builder.layers.Add(layer);
        }
    }
}