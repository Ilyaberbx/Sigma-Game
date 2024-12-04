using System;
using Odumbrata.Behaviour.Levels.Modules;

namespace Odumbrata.Core.Modules.Management
{
    public class ModulesFactory<TDerivedModule> where TDerivedModule : BaseBehaviourModule
    {
        private readonly EventSystem.EventSystem _events;
        private readonly Type _contextType;

        public ModulesFactory(EventSystem.EventSystem events,
            Type contextType)
        {
            _events = events;
            _contextType = contextType;
        }

        public TModule Create<TModule>() where TModule : class, TDerivedModule, new()
        {
            var module = new TModule();
            module.Initialize(_contextType, _events);
            return module;
        }

        public TModule CreateWithConfiguration<TModule, TConfig>(TConfig config)
            where TModule : class, TDerivedModule, IConfigurableModule<TConfig>, new()
        {
            var module = new TModule();
            module.SetConfiguration(config);
            module.Initialize(_contextType, _events);
            return module;
        }

        public TModule CreateWithRuntime<TModule, TRuntimeData>(TRuntimeData runtime)
            where TModule : class, TDerivedModule, IRuntimeDataModule<TRuntimeData>, new()
        {
            var module = new TModule();
            module.SetRuntime(runtime);
            module.Initialize(_contextType, _events);
            return module;
        }

        public TModule CreateFullSetup<TModule, TConfig, TRuntimeData>(TConfig config, TRuntimeData runtime)
            where TModule : class, TDerivedModule, IConfigurableModule<TConfig>, IRuntimeDataModule<TRuntimeData>, new
            ()
        {
            var module = new TModule();
            module.SetConfiguration(config);
            module.SetRuntime(runtime);
            module.Initialize(_contextType, _events);
            return module;
        }
    }
}