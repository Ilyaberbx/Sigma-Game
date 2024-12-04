using System;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Behaviour.Rooms.Debug;
using Odumbrata.Core.EventSystem;
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

    public abstract class BaseRoomBehaviour : BaseBehaviour, IDisposable
    {
        [SerializeField] private DoorsModuleConfig _doorsModuleConfig;
        [SerializeField] private DebugRoomModuleConfig _debugRoomModuleConfig;
        [SerializeField] private RoomsSelectionModuleConfig _roomsSelectionModuleConfig;

        public bool IsActive { get; private set; }
        private EventSystem _events;

        public void Initialize(EventSystem events)
        {
            _events = events;
            InitializeModules();
            _events.Publish(this, new RoomInitializedArg(GetType()));
        }

        public virtual void Dispose()
        {
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

        protected void AddModule<TModule>() where TModule : BaseRoomModule, new()
        {
            var module = new TModule();
            module.Initialize(GetType(), _events);
        }

        protected void AddModule<TModule, TConfig>(TConfig config) where TModule : BaseRoomModule<TConfig>, new()
        {
            var module = new TModule();
            module.SetConfiguration(config);
            module.Initialize(GetType(), _events);
        }

        protected void AddModule<TModule, TConfig, TRuntimeData>(TConfig config, TRuntimeData data)
            where TModule : BaseRoomModule<TConfig, TRuntimeData>, new()
        {
            var module = new TModule();
            module.SetConfiguration(config);
            module.SetRuntime(data);
            module.Initialize(GetType(), _events);
        }

        protected virtual void InitializeModules()
        {
            InitializeDoorsModule();
            AddModule<DebugRoomModule, DebugRoomModuleConfig>(_debugRoomModuleConfig);
            AddModule<DoorsRoomSelectionModule, RoomsSelectionModuleConfig>(_roomsSelectionModuleConfig);
        }

        private void InitializeDoorsModule()
        {
            var doorsRuntimeData = new DoorRuntimeData[]
            {
                new()
                {
                    IsOpen = false, IsLocked = false,
                },
                new()
                {
                    IsOpen = false, IsLocked = false,
                },
            };

            AddModule<DoorsCoreModule, DoorsModuleConfig, DoorRuntimeData[]>(_doorsModuleConfig,
                doorsRuntimeData);
        }
    }
}