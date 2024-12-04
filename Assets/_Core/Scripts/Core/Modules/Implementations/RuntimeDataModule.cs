using System;

namespace Odumbrata.Core.Modules
{
    public sealed class RuntimeDataModule<TRuntimeData> : IRuntimeDataModule<TRuntimeData>, IDisposable
    {
        public TRuntimeData RuntimeData { get; private set; }

        public void SetRuntime(TRuntimeData runtime)
        {
            RuntimeData = runtime;
        }

        public void Dispose()
        {
            RuntimeData = default;
        }
    }
}