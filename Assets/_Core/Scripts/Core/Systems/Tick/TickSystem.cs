using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Commons.DataManagement;

namespace Odumbrata.Tick
{
    public sealed class TickSystem<TTickable> : ISubscriptionHandler<TTickable>, ITickable, ISystem where TTickable : ITickable
    {
        private readonly Register<TTickable> _tickablesRegister;
        private IReadOnlyList<TTickable> Tickables => _tickablesRegister.Elements;
        
        public TickSystem()
        {
            _tickablesRegister = new();
        }


        #region ITickale

        public void Tick(float deltaTime)
        {
            if (Tickables.IsEmpty())
            {
                return;
            }

            foreach (var tickable in Tickables)
            {
                tickable.Tick(deltaTime);
            }
        }

        #endregion

        #region ISubscriptionHandler

        public void Subscribe(TTickable tickable) => _tickablesRegister.Subscribe(tickable);

        public void Unsubscribe(TTickable tickable) => _tickablesRegister.Unsubscribe(tickable);

        #endregion
    }
}