using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Odumbrata.Core;

namespace Odumbrata.Global.Services
{
    public abstract class BaseTickService<TTickable> : MonoService, IRegister<TTickable>, ITickable
        where TTickable : ITickable
    {
        private TickSystem<TTickable> _tickSystem;

        public bool Contains(TTickable element)
        {
            return _tickSystem.Contains(element);
        }

        public IReadOnlyList<TTickable> Elements => _tickSystem.Elements;

        protected sealed override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _tickSystem = new TickSystem<TTickable>();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #region IRegister

        public void Add(TTickable tickable)
        {
            _tickSystem.Add(tickable);
        }

        public void Remove(TTickable tickable)
        {
            _tickSystem.Remove(tickable);
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