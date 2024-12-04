using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Core.EventSystem;

namespace Odumbrata.Behaviour.Levels
{
    public abstract class BaseLevelBehaviour : BaseLevelBehaviour<object>
    {
        private EventSystem _events;

        public virtual Task Enter()
        {
            _events = new EventSystem();

            return Task.CompletedTask;
        }

        public override Task Exit()
        {
            _events.Dispose();
            _events = null;

            return Task.CompletedTask;
        }

        public sealed override Task Enter(object data)
        {
            return Enter();
        }

        protected void AddModule<TModule>() where TModule : BaseLevelModule, new()
        {
            var module = new TModule();
            module.Initialize(GetType(), _events);
        }

        protected void AddModule<TModule, TConfig>(TConfig config) where TModule : BaseLevelModule<TConfig>, new()
        {
            var module = new TModule();
            module.SetConfiguration(config);
            module.Initialize(GetType(), _events);
        }

        protected void AddModule<TModule, TConfig, TRuntimeData>(TConfig config, TRuntimeData data)
            where TModule : BaseLevelModule<TConfig, TRuntimeData>, new()
        {
            var module = new TModule();
            module.SetConfiguration(config);
            module.SetRuntime(data);
            module.Initialize(GetType(), _events);
        }
    }

    public abstract class BaseLevelBehaviour<TData> : BaseBehaviour
    {
        public abstract Task Enter(TData data);
        public abstract Task Exit();
    }
}