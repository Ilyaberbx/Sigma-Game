using System;
using System.Threading;
using System.Threading.Tasks;
using Better.StateMachine.Runtime.States;
using Odumbrata.Core.Container;
using Odumbrata.Features.Movement.Data;

namespace Odumbrata.Features.Movement.States
{
    [Serializable]
    public abstract class BaseMove : BaseState
    {
        protected ISystemsContainerReadonly Container { get; private set; }

        public virtual void Initialize(ISystemsContainerReadonly container)
        {
            Container = container;
        }

        public sealed override Task EnterAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public sealed override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }

    public abstract class BaseMove<TData> : BaseMove where TData : BaseMoveData
    {
        protected TData Data { get; private set; }

        public void SetData(TData data)
        {
            Data = data;
        }
    }
}