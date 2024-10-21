using System;
using System.Threading.Tasks;
using Odumbrata.Behaviour.Common.Door;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels.Modules
{
    [Serializable]
    public class DoorsModuleConfig
    {
        [SerializeField] private BaseDoorBehaviour[] _doors;

        public BaseDoorBehaviour[] Doors => _doors;
    }

    public class DoorsModule : BaseLevelModule<DoorsModuleConfig>
    {
        private const int ToMillisecondsEquivalent = 1000;

        public override void Initialize()
        {
            base.Initialize();

            foreach (var door in Config.Doors)
            {
                door.OnHandlerEnter += OnDoorsTransition;
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (var door in Config.Doors)
            {
                door.OnHandlerEnter -= OnDoorsTransition;
            }
        }

        private async void OnDoorsTransition(IDoorHandler handler, BaseDoorBehaviour door)
        {
            if (door.IsOpened)
            {
                return;
            }

            var interactionSide = GetSide(door, handler);
            var interactionPosition = door.GetInteractionPosition(interactionSide);
            var data = new DoorInteractionData(interactionPosition);

            door.SetActiveCollision(false);
            await handler.HandleDoorOpeningStarted(data);
            await door.Open();
            await handler.HandleDoorOpeningEnded(data);

            if (door.LeftOpenedAfterTransition)
            {
                return;
            }

            await Task.Delay((int)door.DelayBeforeClosing * ToMillisecondsEquivalent);
            await door.Close();
            door.SetActiveCollision(true);
        }

        private Side GetSide(BaseDoorBehaviour door, IDoorHandler handler)
        {
            var doorPosition = door.Transform.position;
            var handlerPosition = handler.Transform.position;
            var side = doorPosition.x >= handlerPosition.x ? Side.Back : Side.Front;
            return side;
        }
    }
}