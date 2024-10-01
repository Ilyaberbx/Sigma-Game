using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Odumbrata.Core.Container;
using UnityEngine;

namespace Odumbrata.Core
{
    public sealed class SystemsContainer : ISystemsContainer
    {
        private readonly Locator<ISystem> _locator = new();
        public void Add<TSystem>(params TSystem[] systems) where TSystem : BaseSystem
        {
            if (systems.IsNullOrEmpty())
            {
                return;
            }

            foreach (var system in systems)
            {
                _locator.Add(system);

                Debug.Log("System added: " + system.GetType().Name);
            }

            foreach (var system in systems)
            {
                system.Initialize(this);

                Debug.Log("System initialized: " + system.GetType().Name);
            }
        }

        public void Remove<TSystem>(params TSystem[] systems) where TSystem : BaseSystem
        {
            foreach (var system in systems)
            {
                system.Dispose();

                _locator.Remove(system);

                Debug.Log("System removed: " + system.GetType().Name);
            }
        }

        public TSystem GetSystem<TSystem>() where TSystem : BaseSystem
        {
            return (TSystem)_locator.Get(typeof(TSystem));
        }

        public bool TryGetSystem<TSystem>(out TSystem system) where TSystem : BaseSystem
        {
            return _locator.TryGet(typeof(TSystem), out system);
        }

        public bool HasSystem<TSystem>() where TSystem : BaseSystem
        {
            return _locator.ContainsKey(typeof(TSystem));
        }
    }
}