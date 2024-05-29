using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Odumbrata.Tick;
using UnityEngine;

namespace Odumbrata.Services.Updates
{
    public class UpdateService : MonoService, ITickRegistry<IUpdatable>
    {
        private TickSystem<IUpdatable> _tickSystem;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _tickSystem = new TickSystem<IUpdatable>();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void Update()
        {
            if (false) //TODO: PauseService check
            {
                return;
            }

            _tickSystem.Tick(Time.deltaTime);
        }

        #region ITickRegistry

        public void Subscribe(IUpdatable tickable)
        {
            _tickSystem.Subscribe(tickable);
        }

        public void Unsubscribe(IUpdatable tickable)
        {
            _tickSystem.Unsubscribe(tickable);
        }

        #endregion
    }
}