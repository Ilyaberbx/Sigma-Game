using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Levels.Modules;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels
{
    public abstract class BaseLevelBehaviour : BaseLevelBehaviour<object>
    {
        [SerializeReference, Select] private BaseLevelModule[] _modulesToAdd;

        private List<BaseLevelModule> _activeModules;

        public virtual Task Enter()
        {
            _activeModules = new List<BaseLevelModule>();

            foreach (var module in _modulesToAdd)
            {
                module.Initialize();
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

        public sealed override Task Enter(object data)
        {
            return Enter();
        }
    }

    public abstract class BaseLevelBehaviour<TData> : BaseBehaviour
    {
        public abstract Task Enter(TData data);
        public abstract Task Exit();
    }
}