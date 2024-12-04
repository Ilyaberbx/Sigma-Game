using System;
using System.Threading;
using Odumbrata.Core.EventSystem;

namespace Odumbrata.Behaviour.Levels.Modules
{
    #region Core

    [Serializable]
    public abstract class BaseEntityModule<TConfig> : BaseEntityModule
    {
        protected TConfig Config => _config;
        private TConfig _config;

        public void SetConfiguration(TConfig config)
        {
            _config = config;
        }
    }

    [Serializable]
    public abstract class BaseEntityModule
    {
        private CancellationTokenSource _disposeCancellationSource;
        protected EventSystem Events { get; private set; }
        protected CancellationToken DisposeCancellationToken => _disposeCancellationSource.Token;
        protected Type ContextType { get; private set; }

        public virtual void Initialize(Type context, EventSystem events)
        {
            ContextType = context;
            Events = events;
            _disposeCancellationSource = new CancellationTokenSource();
        }

        public virtual void Dispose()
        {
            _disposeCancellationSource?.Cancel();
        }

        protected bool IsContext(Type type)
        {
            return ContextType == type;
        }
    }

    public abstract class BaseEntityModule<TConfig, TRuntimeData> : BaseEntityModule<TConfig>
    {
        protected TRuntimeData RuntimeData;

        public void SetRuntime(TRuntimeData data)
        {
            RuntimeData = data;
        }
    }

    #endregion

    #region Variations

    #region Level

    [Serializable]
    public abstract class BaseLevelModule : BaseEntityModule
    {
    }

    [Serializable]
    public abstract class BaseLevelModule<TConfig> : BaseEntityModule<TConfig>
    {
    }

    [Serializable]
    public abstract class BaseLevelModule<TConfig, TRuntimeData> : BaseEntityModule<TConfig, TRuntimeData>
    {
    }

    #endregion

    #region Room

    [Serializable]
    public abstract class BaseRoomModule : BaseEntityModule
    {
    }

    [Serializable]
    public abstract class BaseRoomModule<TConfig> : BaseEntityModule<TConfig>
    {
    }

    [Serializable]
    public abstract class BaseRoomModule<TConfig, TRuntimeData> : BaseEntityModule<TConfig, TRuntimeData>
    {
    }

    #endregion

    #endregion
}