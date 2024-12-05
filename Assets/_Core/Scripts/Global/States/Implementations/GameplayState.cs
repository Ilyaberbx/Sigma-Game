using System.Threading;
using System.Threading.Tasks;

namespace Odumbrata.Global.States
{
    public sealed class GameplayState : BaseGameState
    {
        public override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}