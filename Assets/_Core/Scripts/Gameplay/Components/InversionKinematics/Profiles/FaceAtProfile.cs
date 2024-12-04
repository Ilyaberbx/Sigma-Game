using Better.Locators.Runtime;
using Odumbrata.Components.InversionKinematics.Contexts;
using Odumbrata.Global.Services;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Odumbrata.Components.InversionKinematics.Profiles
{
    public class FaceAtProfile : BaseIkProfile<Vector3>, IUpdatable
    {
        private UpdateService _updateService;

        public override void Initialize(IHumanoidContext humanoidContext, Vector3 data)
        {
            base.Initialize(humanoidContext, data);

            _updateService = ServiceLocator.Get<UpdateService>();
        }

        public override void Apply(RigBuilder builder)
        {
            _updateService.Add(this);

            var rig = HumanoidContext.LookAtRig;
            rig.weight = 0;

            var layer = new RigLayer(rig);
            builder.layers.Add(layer);
        }

        public override void Dispose()
        {
            base.Dispose();

            _updateService.Remove(this);
        }

        public void Tick(float deltaTime)
        {
            if (!IsInitialized)
            {
                return;
            }

            HumanoidContext.LookAt(Data);
        }
    }
}