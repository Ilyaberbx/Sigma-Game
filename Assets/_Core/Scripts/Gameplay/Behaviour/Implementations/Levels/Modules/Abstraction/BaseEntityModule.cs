using System;
using System.Threading;
using Odumbrata.Core.EventSystem;

namespace Odumbrata.Behaviour.Levels.Modules
{
    #region Core

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

    #endregion

    #region Variations

    #region Level

    [Serializable]
    public abstract class BaseLevelModule : BaseEntityModule
    {
    }

    #endregion

    #region Room

    [Serializable]
    public abstract class BaseRoomModule : BaseEntityModule
    {
    }

    #endregion

    #endregion
}