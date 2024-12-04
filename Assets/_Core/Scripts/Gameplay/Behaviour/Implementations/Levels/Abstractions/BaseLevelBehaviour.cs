using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Core.EventSystem;
using Odumbrata.Core.Modules.Management;

namespace Odumbrata.Behaviour.Levels
{
    public abstract class BaseLevelBehaviour : BaseBehaviour, IModuleContainer<BaseLevelModule>
    {
        private EventSystem _events;
        private IModuleContainer<BaseLevelModule> _container;
        protected ModulesFactory<BaseLevelModule> Factory { get; private set; }

        public virtual Task Enter()
        {
            _events = new EventSystem();
            _container = new ModuleContainer<BaseLevelModule>();
            Factory = new ModulesFactory<BaseLevelModule>(_events, GetType());
            return Task.CompletedTask;
        }

        public virtual Task Exit()
        {
            _events.Dispose();
            _events = null;

            return Task.CompletedTask;
        }

        public void AddModule<TModule>(TModule module) where TModule : BaseLevelModule
        {
            _container.AddModule(module);
        }

        public void RemoveModule<TModule>() where TModule : BaseLevelModule
        {
            var module = _container.GetModule<TModule>();

            if (module == null)
            {
                return;
            }

            module.Dispose();
            _container.RemoveModule<TModule>();
        }

        public void Clear()
        {
            _container.Clear();
        }

        public TModule GetModule<TModule>() where TModule : BaseLevelModule
        {
            return _container.GetModule<TModule>();
        }
    }
}