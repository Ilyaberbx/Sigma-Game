using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Systems.Movement.States;

namespace Odumbrata.Core
{
    public sealed class TickSystem<TTickable> : IRegister<TTickable>, ITickable, ISystem
        where TTickable : ITickable
    {
        public bool IsInitialized => _register != null;

        private readonly Register<TTickable> _register = new();
        public IReadOnlyList<TTickable> Elements => _register.Elements;

        #region ITickale

        public void Tick(float deltaTime)
        {
            if (Elements.IsNullOrEmpty() || !IsInitialized)
            {
                return;
            }

            foreach (var tickable in Elements.ToList())
            {
                tickable.Tick(deltaTime);
            }
        }

        #endregion

        #region ISubscriptionHandler

        public void Add(TTickable tickable) => _register.Add(tickable);

        public void Remove(TTickable tickable) => _register.Remove(tickable);

        #endregion
    }
}