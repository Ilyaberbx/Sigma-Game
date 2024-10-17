using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;
using UnityPhysics = UnityEngine.Physics;

namespace Odumbrata.Services.Physics
{
    public class PhysicsService : PocoService
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public bool TryRaycast(Ray ray, out RaycastHit info)
        {
            return UnityPhysics.Raycast(ray, out info);
        }
    }
}