using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Odumbrata.Core.EventSystem;

namespace Odumbrata.Services.Levels
{
    public sealed class LevelsService : PocoService
    {
        public EventSystem Events { get; private set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Events = new EventSystem();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}