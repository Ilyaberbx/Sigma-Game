using Better.Locators.Runtime;
using Odumbrata.Features.InversionKinematics.Contexts;
using Odumbrata.Global.Services;
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

    public class GrabProfile : BaseIkProfile<GrabData>, IUpdatable
    {
        private UpdateService _updateService;

        public override void Initialize(IHumanoidContext humanoid, GrabData data)
        {
            base.Initialize(humanoid, data);

            _updateService = ServiceLocator.Get<UpdateService>();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_updateService.Contains(this))
            {
                _updateService.Remove(this);
            }
        }

        public override void Apply(RigBuilder builder)
        {
            var rig = Data.HandType == HandType.Left
                ? HumanoidContext.LeftHandGrabbingRig
                : HumanoidContext.RightHandGrabbingRig;

            rig.weight = 0;

            HumanoidContext.Grab(Data.HandType, Data.Position);

            var layer = new RigLayer(rig);
            builder.layers.Add(layer);

            _updateService.Add(this);
        }

        public void Tick(float deltaTime)
        {
            HumanoidContext.Grab(Data.HandType, Data.Position);
        }
    }
}