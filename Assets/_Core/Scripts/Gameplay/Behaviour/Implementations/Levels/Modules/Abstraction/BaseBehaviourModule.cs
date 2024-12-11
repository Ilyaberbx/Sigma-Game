using System;
using System.Threading;
using System.Threading.Tasks;
using Odumbrata.Core.EventSystem;

namespace Odumbrata.Behaviour.Levels.Modules
{
    #region Core

    [Serializable]
    public abstract class BaseBehaviourModule
    {
        private CancellationTokenSource _disposeCancellationSource;
        protected EventSystem Events { get; private set; }
        protected CancellationToken DisposeCancellationToken => _disposeCancellationSource.Token;
        protected Type ContextType { get; private set; }

        public virtual Task Initialize(Type context, EventSystem events)
        {
            ContextType = context;
            Events = events;
            _disposeCancellationSource = new CancellationTokenSource();
            return Task.CompletedTask;
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
    public abstract class BaseLevelModule : BaseBehaviourModule
    {
    }

    #endregion

    #region Room

    [Serializable]
    public abstract class BaseRoomModule : BaseBehaviourModule
    {
    }

    #endregion

    #endregion
}