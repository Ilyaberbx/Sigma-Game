using System;
using System.Linq;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Odumbrata.Behaviour.Common.Door;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Core.EventSystem;
using Odumbrata.Core.Modules;
using Odumbrata.Data.Static;
using Odumbrata.Services.Rooms;

namespace Odumbrata.Behaviour.Rooms
{
    public sealed class RoomsActivationModule : BaseRoomModule, IConfigurableModule<RoomsActivationModuleConfig>
    {
        private RoomsService _roomsService;
        private readonly ConfigurableModule<RoomsActivationModuleConfig> _configurable = new();

        public RoomsActivationModuleConfig Config => _configurable.Config;

        public override async Task Initialize(Type context, EventSystem events)
        {
            await base.Initialize(context, events);

            _roomsService = ServiceLocator.Get<RoomsService>();

            Events.Subscribe<DoorPreOpenArg>(OnDoorPreOpen);
        }

        public override void Dispose()
        {
            base.Dispose();

            _configurable.Dispose();
            Events.Unsubscribe<DoorPreOpenArg>(OnDoorPreOpen);
        }

        public void SetConfiguration(RoomsActivationModuleConfig config)
        {
            _configurable.SetConfiguration(config);
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

        private bool TryGetSelectionData(BaseDoorBehaviour door, out RoomsActivationConfig activationData)
        {
            activationData = Config
                .ActivationsData
                .FirstOrDefault(temp => temp.Door == door);

            return activationData != null;
        }
    }
}