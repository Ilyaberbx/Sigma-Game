using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Odumbrata.Commons.DataManagement;
using Odumbrata.Tick;
using UnityEngine;

namespace Odumbrata.Services.Updates
{
    public class FixedUpdateService : MonoService, ISubscriptionHandler<IFixedUpdatable>
    {
        private TickSystem<IFixedUpdatable> _tickSystem;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _tickSystem = new TickSystem<IFixedUpdatable>();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void FixedUpdate()
        {
            if (false) //TODO: PauseService check
            {
                return;
            }

            _tickSystem.Tick(Time.fixedDeltaTime);
        }

        #region ITickRegistry

        public void Subscribe(IFixedUpdatable tickable)
        {
            _tickSystem.Subscribe(tickable);
        }

        public void Unsubscribe(IFixedUpdatable tickable)
        {
            _tickSystem.Unsubscribe(tickable);
        }

        #endregion
    }
}