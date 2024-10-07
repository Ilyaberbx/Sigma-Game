using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;

namespace Odumbrata.Services.Camera
{
    public class CameraService : PocoService
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}