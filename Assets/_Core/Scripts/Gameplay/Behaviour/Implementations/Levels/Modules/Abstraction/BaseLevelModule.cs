using System;
using System.Threading;
using Odumbrata.Core.EventSystem;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels.Modules
{
    [Serializable]
    public abstract class BaseLevelModule
    {
        protected EventSystem Events { get; private set; }
        protected CancellationToken DisposeCancellationToken => _disposeCancellationSource.Token;
        private CancellationTokenSource _disposeCancellationSource;

        public virtual void Initialize(EventSystem events)
        {
            Events = events;
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