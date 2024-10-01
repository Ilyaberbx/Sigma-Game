using System;
using System.Threading;
using System.Threading.Tasks;
using Better.StateMachine.Runtime.States;
using Odumbrata.Core.Container;

namespace Odumbrata.Behaviour
{
    [Serializable]
    public abstract class BaseBehaviourState : BaseState
    {
        protected ISystemsContainer Container { get; private set; }

        public virtual void Initialize(ISystemsContainer container)
        {
            Container = container;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}