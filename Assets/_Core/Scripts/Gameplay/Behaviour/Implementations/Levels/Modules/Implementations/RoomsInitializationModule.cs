using System;
using Better.Locators.Runtime;
using Odumbrata.Core.EventSystem;
using Odumbrata.Core.Modules;
using Odumbrata.Services.Rooms;

namespace Odumbrata.Behaviour.Levels.Modules
{
    public sealed class RoomsInitializationModule : BaseLevelModule, IConfigurableModule<Data.Static.RoomsInitializationModule>
    {
        private RoomsService _roomsService;
        private readonly ConfigurableModule<Data.Static.RoomsInitializationModule> _configurable = new();
        public Data.Static.RoomsInitializationModule Config => _configurable.Config;

        public override void Initialize(Type context, EventSystem events)
        {
            base.Initialize(context, events);

            _roomsService = ServiceLocator.Get<RoomsService>();

            foreach (var room in Config.Rooms)
            {
                room.Initialize(events);
                _roomsService.Add(room);
            }

            foreach (var room in Config.Rooms)
            {
                _roomsService.Deactivate(room.GetType());
            }

            foreach (var activeOnStart in Config.ActiveOnStart)
            {
                var roomType = activeOnStart.Type;
                _roomsService.Activate(roomType, RoomTransitionType.Additional);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (var room in Config.Rooms)
            {
                room.Dispose();
                _roomsService.Remove(room);
            }

            _configurable.Dispose();
        }

        public void SetConfiguration(Data.Static.RoomsInitializationModule config)
        {
            _configurable.SetConfiguration(config);
        }
    }
}