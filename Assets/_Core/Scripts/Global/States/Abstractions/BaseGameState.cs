using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Odumbrata.Behaviour;
using Odumbrata.Global.Services;

namespace Odumbrata.Global.States
{
    public abstract class BaseGameState : BaseEntityState
    {
        protected GameService GameService { get; private set; }

        public override Task EnterAsync(CancellationToken token)
        {
            GameService = ServiceLocator.Get<GameService>();
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