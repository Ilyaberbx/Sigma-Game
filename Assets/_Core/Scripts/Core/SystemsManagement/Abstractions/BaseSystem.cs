using System;
using System.Threading;
using Odumbrata.Core.Container;

namespace Odumbrata.Core
{
    [Serializable]
    public abstract class BaseSystem : ISystem, IDisposable
    {
        public bool IsInitialized => Container != null;

        protected CancellationTokenSource _cancellationTokenSource;
        protected ISystemsContainerReadonly Container { get; private set; }
        protected CancellationToken DisposeCancellation => _cancellationTokenSource.Token;

        public virtual void Initialize(ISystemsContainerReadonly container)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Container = container;
        }

        public virtual void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            Container = null;
        }
    }
}