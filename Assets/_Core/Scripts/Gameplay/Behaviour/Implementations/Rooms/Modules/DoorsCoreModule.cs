using System;
using System.Threading.Tasks;
using Odumbrata.Behaviour.Common.Door;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Core.EventSystem;
using Odumbrata.Core.Modules;
using Odumbrata.Data.Runtime;
using Odumbrata.Data.Static;
using UnityEngine;

namespace Odumbrata.Behaviour.Rooms
{
    #region Events

    public struct DoorOpenedArg : IEventArg
    {
        public BaseDoorBehaviour DoorBehaviour { get; }
        public bool IsFront { get; }

        public DoorOpenedArg(BaseDoorBehaviour doorBehaviour, bool isFront)
        {
            DoorBehaviour = doorBehaviour;
            IsFront = isFront;
        }
    }

    public struct DoorPreOpenArg : IEventArg
    {
        public BaseDoorBehaviour DoorBehaviour { get; }
        public bool IsFront { get; }

        public DoorPreOpenArg(BaseDoorBehaviour doorBehaviour, bool isFront)
        {
            DoorBehaviour = doorBehaviour;
            IsFront = isFront;
        }
    }

    public struct DoorClosedArg : IEventArg
    {
        public BaseDoorBehaviour DoorBehaviour { get; }

        public bool IsFront { get; }

        public DoorClosedArg(BaseDoorBehaviour doorBehaviour, bool isFront)
        {
            DoorBehaviour = doorBehaviour;
            IsFront = isFront;
        }
    }

    #endregion

    public sealed class DoorsCoreModule : BaseRoomModule, IConfigurableModule<DoorsModuleConfig>,
        IRuntimeDataModule<DoorRuntimeData[]>
    {
        private const int ToMillisecondsEquivalent = 1000;

        private readonly ConfigurableModule<DoorsModuleConfig> _configurableModule = new();
        private readonly RuntimeDataModule<DoorRuntimeData[]> _runtimeDataModule = new();
        public DoorsModuleConfig Config => _configurableModule.Config;
        public DoorRuntimeData[] RuntimeData => _runtimeDataModule.RuntimeData;

        public override void Initialize(Type context, EventSystem events)
        {
            base.Initialize(context, events);

            for (var i = 0; i < Config.Doors.Length; i++)
            {
                var door = Config.Doors[i];
                var data = RuntimeData[i];
                door.SetData(data);
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

            _configurableModule.Dispose();
            _runtimeDataModule.Dispose();
        }

        public void SetConfiguration(DoorsModuleConfig config)
        {
            _configurableModule.SetConfiguration(config);
        }

        public void SetRuntime(DoorRuntimeData[] runtime)
        {
            _runtimeDataModule.SetRuntime(runtime);
        }

        private async void OnDoorsTransition(IDoorHandler handler, BaseDoorBehaviour door)
        {
            if (door.IsOpened)
            {
                return;
            }

            var isFront = IsFront(handler, door);
            var transitionPosition = isFront ? door.FrontInteractionPosition : door.BackInteractionPosition;
            var lookAt = door.LookAtPoint.position;
            var data = new DoorHandleData(transitionPosition, lookAt, Config.TransitionDuration);

            await handler.HandleDoorPreOpening(data);
            Events.Publish(this, new DoorPreOpenArg(door, isFront));
            await door.Open();
            Events.Publish(this, new DoorOpenedArg(door, isFront));

            if (door.LeftOpenedAfterTransition)
            {
                return;
            }

            door.FinishInteraction();

            var delay = (int)door.DelayBeforeClosing * ToMillisecondsEquivalent;

            await Task.Delay(delay);
            await door.Close();
            Events.Publish(this, new DoorClosedArg(door, isFront));
        }

        private bool IsFront(IDoorHandler handler, BaseDoorBehaviour door)
        {
            var handlerPosition = handler.Position;

            var frontDistance = Vector3.Distance(handlerPosition, door.FrontInteractionPosition);
            var backDistance = Vector3.Distance(handlerPosition, door.BackInteractionPosition);

            return frontDistance < backDistance;
        }
    }
}