using System;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Behaviour.Rooms.Debug;
using Odumbrata.Core.EventSystem;
using Odumbrata.Core.Modules.Management;
using Odumbrata.Data.Runtime;
using Odumbrata.Data.Static;
using UnityEngine;

namespace Odumbrata.Behaviour.Rooms.Abstractions
{
    #region Events

    public struct RoomActivatedArg : IEventArg
    {
        public Type ContextType { get; private set; }

        public RoomActivatedArg(Type contextType)
        {
            ContextType = contextType;
        }
    }

    public struct RoomDeactivatedArg : IEventArg
    {
        public Type ContextType { get; private set; }

        public RoomDeactivatedArg(Type contextType)
        {
            ContextType = contextType;
        }
    }

    public sealed class RoomInitializedArg : IEventArg
    {
        public Type ContextType { get; private set; }

        public RoomInitializedArg(Type contextType)
        {
            ContextType = contextType;
        }
    }

    #endregion

    public abstract class BaseRoomBehaviour : BaseBehaviour, IDisposable, IModuleContainer<BaseRoomModule>
    {
        [SerializeField] private DoorsInitializationModuleConfig _doorsInitializationModuleConfig;
        [SerializeField] private RoomsActivationModuleConfig _roomsActivationModuleConfig;
        [SerializeField] private DebugRoomModuleConfig _debugRoomModuleConfig;

        private IModuleContainer<BaseRoomModule> _container;
        private EventSystem _events;
        public bool IsActive { get; private set; }
        private ModuleFactory<BaseRoomModule> Factory { get; set; }

        public void Initialize(EventSystem events)
        {
            _events = events;
            _container = new ModuleContainer<BaseRoomModule>();
            Factory = new ModuleFactory<BaseRoomModule>(_events, GetType());
            InitializeModules();
            _events.Publish(this, new RoomInitializedArg(GetType()));
        }

        public void Dispose()
        {
            DisposeModules();
            _events.Dispose();
            _events = null;
        }

        public void Activate()
        {
            IsActive = true;
            _events.Publish(this, new RoomActivatedArg(GetType()));
        }

        public void Deactivate()
        {
            IsActive = false;
            _events.Publish(this, new RoomDeactivatedArg(GetType()));
        }

        protected virtual async void InitializeModules()
        {
            var debugModule = await Factory.CreateWithConfiguration<DebugRoomModule,
                DebugRoomModuleConfig>(_debugRoomModuleConfig);

            var doorsRuntimeData = new[]
            {
                new DoorRuntimeData()
                {
                    IsLocked = false,
                    IsOpened = false,
                }
            };

            var doorsCoreModule = await Factory.CreateFullSetup<DoorsInitializationModule,
                DoorsInitializationModuleConfig, DoorRuntimeData[]>(_doorsInitializationModuleConfig, doorsRuntimeData);

            var roomsActivationModule = await Factory.CreateWithConfiguration<RoomsActivationModule,
                RoomsActivationModuleConfig>(_roomsActivationModuleConfig);

            AddModule(debugModule);
            AddModule(doorsCoreModule);
            AddModule(roomsActivationModule);
        }

        protected virtual void DisposeModules()
        {
            RemoveModule<DebugRoomModule>();
            RemoveModule<DoorsInitializationModule>();
            RemoveModule<RoomsActivationModule>();
        }

        public void AddModule<TModule>(TModule module) where TModule : BaseRoomModule
        {
            _container.AddModule(module);
        }

        public void RemoveModule<TModule>() where TModule : BaseRoomModule
        {
            var module = _container.GetModule<TModule>();

            if (module == null)
            {
                return;
            }

            module.Dispose();
            _container.RemoveModule<TModule>();
        }

        public TModule GetModule<TModule>() where TModule : BaseRoomModule
        {
            return _container.GetModule<TModule>();
        }

        public void Clear()
        {
            _container.Clear();
        }
    }
}