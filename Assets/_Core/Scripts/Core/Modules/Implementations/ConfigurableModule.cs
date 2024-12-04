using System;

namespace Odumbrata.Core.Modules
{
    public sealed class ConfigurableModule<TConfig> : IConfigurableModule<TConfig>, IDisposable
    {
        public TConfig Config { get; private set; }

        public void SetConfiguration(TConfig config)
        {
            Config = config;
        }

        public void Dispose()
        {
            Config = default;
        }
    }
}