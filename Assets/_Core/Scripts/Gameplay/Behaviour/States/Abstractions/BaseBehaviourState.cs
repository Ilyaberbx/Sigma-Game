using System.IO;
using Better.Commons.Runtime.Utility;
using Odumbrata.Core.Container;
using Odumbrata.Entity;

namespace Odumbrata.Behaviour
{
    public abstract class BaseBehaviourState : BaseBehaviourState<object>
    {
        public sealed override void Initialize(ISystemsContainer container, object data)
        {
            base.Initialize(container, data);

            Initialize(container);
        }

        protected abstract void Initialize(ISystemsContainer container);
    }

    public abstract class BaseBehaviourState<TData> : BaseEntityState
    {
        protected TData Data { get; private set; }
        protected ISystemsContainer Container { get; private set; }

        public virtual void Initialize(ISystemsContainer container, TData data)
        {
            Data = data;
            Container = container;
        }

        public override void OnEntered()
        {
        }

        public override void OnExited()
        {
        }
    }
}