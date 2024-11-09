using System;
using System.Threading.Tasks;
using Odumbrata.Behaviour.Common.Door;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels.Modules
{
    [Serializable]
    public class DoorsModuleConfig
    {
        [SerializeField] private float _preOpenDuration;
        [SerializeField] private BaseDoorBehaviour[] _doors;

        public BaseDoorBehaviour[] Doors => _doors;
        public float PreOpenDuration => _preOpenDuration;
    }

    [Serializable]
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

            var transitionPosition = door.GetInteractionPosition(handler);
            var lookAt = door.LookAtPoint.position;
            var data = new DoorTransitionData(transitionPosition, lookAt, Config.PreOpenDuration);

            door.SetActiveObservers(false);
            await handler.HandleDoorPreOpening(data);
            await door.Open();
            await handler.HandleDoorPostOpening(data);

            if (door.LeftOpenedAfterTransition)
            {
                return;
            }

            await Task.Delay((int)door.DelayBeforeClosing * ToMillisecondsEquivalent);
            await door.Close();
            door.SetActiveObservers(true);
        }
    }
}