using System;
using Odumbrata.Core.Container;

namespace Odumbrata.Core
{
    [Serializable]
    public abstract class BaseSystem : ISystem, IDisposable
    {
        public bool IsInitialized => Container != null;
        protected ISystemsContainerReadonly Container { get; private set; }
        
        public virtual void Initialize(ISystemsContainerReadonly container)
        {
            Container = container;
        }

        public virtual void Dispose()
        {
            Container = null;
        }
    }
}