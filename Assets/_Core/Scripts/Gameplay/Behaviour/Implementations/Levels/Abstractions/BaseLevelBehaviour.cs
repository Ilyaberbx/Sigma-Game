using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Core.EventSystem;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels
{
    public abstract class BaseLevelBehaviour : BaseLevelBehaviour<object>
    {
        [SerializeReference, Select] private BaseLevelModule[] _modulesToAdd;

        private List<BaseLevelModule> _activeModules;

        protected virtual Task Enter(EventSystem events)
        {
            _activeModules = new List<BaseLevelModule>();

            foreach (var module in _modulesToAdd)
            {
                module.Initialize(events);
                _activeModules.Add(module);
            }

            return Task.CompletedTask;
        }

        public override Task Exit()
        {
            if (_activeModules.IsEmpty())
            {
                return Task.CompletedTask;
            }

            foreach (var module in _activeModules.ToList())
            {
                module.Dispose();
                _activeModules.Remove(module);
            }

            return Task.CompletedTask;
        }

        public sealed override Task Enter(EventSystem events, object data)
        {
            return Enter(events);
        }
    }

    public abstract class BaseLevelBehaviour<TData> : BaseBehaviour
    {
        public abstract Task Enter(EventSystem events, TData data);
        public abstract Task Exit();
    }
}