using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;

namespace Odumbrata.Tick
{
    public sealed class TickSystem<TTickable> : ITickRegistry<TTickable>, ITickable where TTickable : ITickable
    {
        private readonly List<TTickable> _tickables;

        public TickSystem(List<TTickable> tickables)
        {
            _tickables = tickables;
        }

        public TickSystem()
        {
            _tickables = new();
        }

        public void Subscribe(TTickable tickable)
        {
            _tickables.Add(tickable);
        }

        public void Unsubscribe(TTickable tickable)
        {
            if (tickable == null)
            {
                return;
            }

            if (_tickables.Contains(tickable))
            {
                return;
            }

            _tickables.Remove(tickable);
        }

        public void Tick(float deltaTime)
        {
            if (_tickables.IsEmpty())
            {
                return;
            }

            foreach (var tickable in _tickables)
            {
                tickable.Tick(deltaTime);
            }
        }
    }
}