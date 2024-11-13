using System;
using System.Threading.Tasks;
using Odumbrata.Behaviour.Common.Door;
using Odumbrata.Core.EventSystem;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels.Modules
{
    [Serializable]
    public class DoorData
    {
        [SerializeField] private float _transitionDuration;
        [SerializeField] private BaseDoorBehaviour _door;

        public BaseDoorBehaviour Door => _door;
        public float TransitionDuration => _transitionDuration;
    }

    [Serializable]
    public class DoorsModuleConfig
    {
        [SerializeField] private float _transitionDuration;
        [SerializeField] private BaseDoorBehaviour[] _doors;

        public BaseDoorBehaviour[] Doors => _doors;
        public float TransitionDuration => _transitionDuration;
    }

    [Serializable]
    public sealed class DoorsModule : BaseLevelModule<DoorsModuleConfig>
    {
        private const int ToMillisecondsEquivalent = 1000;

        public override void Initialize(EventSystem events)
        {
            base.Initialize(events);

            foreach (var door in Config.Doors)
            {
                door.OnInteraction += OnDoorsTransition;
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (var door in Config.Doors)
            {
                door.OnInteraction -= OnDoorsTransition;
            }
        }

        private async void OnDoorsTransition(IDoorHandler handler, BaseDoorBehaviour door)
        {
            if (door.IsOpened)
            {
                return;
            }

            var transitionPosition = door.GetInteractionPosition(handler);
            var lookAt = door.LookAtPoint.position;
            var data = new DoorTransitionData(transitionPosition, lookAt, Config.TransitionDuration);

            await handler.HandleDoorPreOpening(data);
            await door.Open();

            if (door.LeftOpenedAfterTransition)
            {
                return;
            }

            door.FireFinishInteraction();

            var delay = (int)door.DelayBeforeClosing * ToMillisecondsEquivalent;

            await Task.Delay(delay);
            await door.Close();
        }
    }
}