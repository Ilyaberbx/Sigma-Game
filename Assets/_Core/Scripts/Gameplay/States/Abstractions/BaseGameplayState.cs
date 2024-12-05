using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime.States;
using Odumbrata.Services;

namespace Odumbrata.States
{
    public abstract class BaseGameplayState : BaseState
    {
        protected GameplayService GameplayService { get; private set; }

        public override Task EnterAsync(CancellationToken token)
        {
            GameplayService = ServiceLocator.Get<GameplayService>();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override void OnEntered()
        {
        }

        public override void OnExited()
        {
        }
    }
}