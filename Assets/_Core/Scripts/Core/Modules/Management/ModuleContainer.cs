using System;
using System.Collections.Generic;
using Odumbrata.Behaviour.Levels.Modules;

namespace Odumbrata.Core.Modules.Management
{
    public sealed class ModuleContainer<TDerivedModule> : IModuleContainer<TDerivedModule>
        where TDerivedModule : BaseEntityModule
    {
        private readonly Dictionary<Type, TDerivedModule> _modules = new();

        public void AddModule<TModule>(TModule module) where TModule : TDerivedModule
        {
            var moduleType = typeof(TModule);

            if (!_modules.TryAdd(moduleType, module))
            {
                throw new InvalidOperationException($"Module of type {moduleType.Name} is already added.");
            }
        }

        public void RemoveModule<TModule>() where TModule : TDerivedModule
        {
            var moduleType = typeof(TModule);

            if (!_modules.ContainsKey(moduleType))
            {
                throw new KeyNotFoundException($"Module of type {moduleType.Name} does not exist.");
            }

            _modules.Remove(moduleType);
        }

        public void Clear()
        {
            _modules.Clear();
        }

        public TModule GetModule<TModule>() where TModule : TDerivedModule
        {
            var moduleType = typeof(TModule);

            if (!_modules.TryGetValue(moduleType, out var module))
            {
                throw new KeyNotFoundException($"Module of type {moduleType.Name} is not registered.");
            }

            return module as TModule;
        }
    }
}