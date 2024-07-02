using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Odumbrata.Core;

namespace Odumbrata.Global.Services
{
    public abstract class BaseTickService<TTickable> : MonoService, ISubscriptionHandler<TTickable>, ITickable
        where TTickable : ITickable
    {
        private TickSystem<TTickable> _tickSystem;

        protected sealed override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _tickSystem = new TickSystem<TTickable>();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #region ISubscriptionHandler

        public void Subscribe(TTickable tickable)
        {
            _tickSystem.Subscribe(tickable);
        }

        public void Unsubscribe(TTickable tickable)
        {
            _tickSystem.Unsubscribe(tickable);
        }

        #endregion

        #region ITickable

        public void Tick(float deltaTime)
        {
            _tickSystem.Tick(deltaTime);
        }

        #endregion
    }
}