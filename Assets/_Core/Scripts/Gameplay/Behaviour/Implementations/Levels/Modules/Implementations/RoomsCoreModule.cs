using System;
using Better.Locators.Runtime;
using Odumbrata.Core.EventSystem;
using Odumbrata.Core.Modules;
using Odumbrata.Data.Static;
using Odumbrata.Services.Rooms;

namespace Odumbrata.Behaviour.Levels.Modules
{
    public sealed class RoomsCoreModule : BaseLevelModule, IConfigurableModule<RoomsCoreModuleConfig>
    {
        private RoomsService _roomsService;
        private readonly ConfigurableModule<RoomsCoreModuleConfig> _configurable = new();
        public RoomsCoreModuleConfig Config => _configurable.Config;

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

            //TODO: Remove Test Data
            var firstRoomType = Config.Rooms[0].GetType();
            _roomsService.Activate(firstRoomType, RoomTransitionType.Additional);
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

        public void SetConfiguration(RoomsCoreModuleConfig config)
        {
            _configurable.SetConfiguration(config);
        }
    }
}