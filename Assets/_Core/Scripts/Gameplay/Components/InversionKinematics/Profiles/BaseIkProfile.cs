using System;
using Odumbrata.Components.InversionKinematics.Contexts;
using UnityEngine.Animations.Rigging;

namespace Odumbrata.Components.InversionKinematics.Profiles
{
    public interface IIkProfile : IDisposable
    {
        public bool IsInitialized { get; }

        public void Apply(RigBuilder builder);
    }

    public abstract class BaseIkProfile<TData> : IIkProfile
    {
        public virtual bool IsInitialized => Data != null;
        protected TData Data { get; private set; }
        protected IHumanoidContext HumanoidContext { get; private set; }

        public virtual void Initialize(IHumanoidContext humanoid, TData data)
        {
            Data = data;
            HumanoidContext = humanoid;
        }

        public virtual void Dispose()
        {
            Data = default(TData);
        }

        public abstract void Apply(RigBuilder builder);
    }
}