using Better.StateMachine.Runtime.States;
using Odumbrata.Core.Container;

namespace Odumbrata.Behaviour
{
    public abstract class BaseEntityState : BaseState
    {
    }

    public abstract class BaseBehaviourState : BaseBehaviourState<object>
    {
        public sealed override void Initialize(ISystemsContainer container, object data)
        {
            base.Initialize(container, data);

            Initialize();
        }

        protected abstract void Initialize();
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