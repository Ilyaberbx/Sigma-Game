using System.Threading;
using System.Threading.Tasks;

namespace Odumbrata.Global.States
{
    public sealed class GameInitializeState : BaseGameState
    {
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