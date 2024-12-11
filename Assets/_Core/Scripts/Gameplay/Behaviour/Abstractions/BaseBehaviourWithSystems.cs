using System;
using System.Linq;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utility;
using Odumbrata.Core;
using Odumbrata.Core.Container;
using UnityEngine;

namespace Odumbrata.Behaviour
{
    public abstract class BaseBehaviourWithSystems : BaseBehaviour, ISystemsContainer
    {
        [SerializeReference, Select(typeof(BaseSystem))]
        private BaseSystem[] _systems;
        private Transform _cachedTransform;
        protected SystemsContainer SystemsContainer { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            SystemsContainer = new SystemsContainer();
        }

        protected virtual void Start()
        {
            InitializeSystems();
        }

        protected virtual void OnDestroy()
        {
            DisposeSystems();
        }

        private void InitializeSystems()
        {
            if (_systems.IsNullOrEmpty())
            {
                return;
            }

            Add(_systems);
        }

        private void DisposeSystems()
        {
            if (_systems.IsNullOrEmpty())
            {
                return;
            }

            Remove(_systems);
        }


        #region ISystemsContext

        public TSystem GetSystem<TSystem>() where TSystem : BaseSystem
        {
            return SystemsContainer.GetSystem<TSystem>();
        }

        public bool TryGetSystem<TSystem>(out TSystem system) where TSystem : BaseSystem
        {
            return SystemsContainer.TryGetSystem(out system);
        }

        public bool HasSystem<TSystem>() where TSystem : BaseSystem
        {
            return SystemsContainer.HasSystem<TSystem>();
        }

        public void Add<TSystem>(params TSystem[] systems) where TSystem : BaseSystem
        {
            SystemsContainer.Add(systems);
        }

        public void Remove<TSystem>(params TSystem[] systems) where TSystem : BaseSystem
        {
            SystemsContainer.Remove(systems);
        }

        #endregion

#if UNITY_EDITOR
        protected void ValidateHasSystem<TSystem>() where TSystem : BaseSystem
        {
            var type = typeof(TSystem);

            if (_systems.Any(system => system.GetType() == type))
            {
                return;
            }

            DebugUtility.LogException<NullReferenceException>("This behaviour requires: " + type.Name);
        }
#endif
    }
}