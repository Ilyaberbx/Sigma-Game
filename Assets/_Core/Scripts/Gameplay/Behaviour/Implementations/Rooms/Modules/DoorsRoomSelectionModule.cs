using System;
using System.Linq;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Odumbrata.Behaviour.Common.Door;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Core.EventSystem;
using Odumbrata.Data.Static;
using Odumbrata.Services.Rooms;

namespace Odumbrata.Behaviour.Rooms
{
    public sealed class DoorsRoomSelectionModule : BaseRoomModule<RoomsSelectionModuleConfig>
    {
        private RoomsService _roomsService;

        public override void Initialize(Type context, EventSystem events)
        {
            base.Initialize(context, events);

            _roomsService = ServiceLocator.Get<RoomsService>();

            Events.Subscribe<DoorPreOpenArg>(OnDoorPreOpen);
        }

        public override void Dispose()
        {
            base.Dispose();

            Events.Unsubscribe<DoorPreOpenArg>(OnDoorPreOpen);
        }

        private void OnDoorPreOpen(object sender, DoorPreOpenArg arg)
        {
            var door = arg.DoorBehaviour;

            if (!TryGetSelectionData(door, out var currentSelectionData))
            {
                return;
            }

            var activate = currentSelectionData.ActivateOnOpen;
            if (activate.IsNullOrEmpty())
            {
                return;
            }

            foreach (var type in activate)
            {
                _roomsService.Activate(type, RoomTransitionType.Additional);
            }
        }

        private bool TryGetSelectionData(BaseDoorBehaviour door, out RoomSelectionConfig currentTransitionData)
        {
            currentTransitionData = Config
                .TransitionsData
                .FirstOrDefault(temp => temp.Door == door);

            return currentTransitionData != null;
        }
    }
}