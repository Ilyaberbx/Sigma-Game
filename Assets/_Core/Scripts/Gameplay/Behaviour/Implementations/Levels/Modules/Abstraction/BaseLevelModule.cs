using System;
using System.Threading;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels.Modules
{
    [Serializable]
    public abstract class BaseLevelModule
    {
        private CancellationTokenSource _disposeCancellationSource;
        protected CancellationToken DisposeCancellationToken => _disposeCancellationSource.Token;

        public virtual void Initialize()
        {
            _disposeCancellationSource = new CancellationTokenSource();
        }

        public virtual void Dispose()
        {
            _disposeCancellationSource?.Cancel();
        }
    }

    [Serializable]
    public abstract class BaseLevelModule<TConfig> : BaseLevelModule
    {
        [SerializeField] private TConfig _config;

        protected TConfig Config => _config;
    }
}