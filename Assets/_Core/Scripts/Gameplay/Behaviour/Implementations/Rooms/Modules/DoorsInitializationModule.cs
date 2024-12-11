using System;
using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Core.EventSystem;
using Odumbrata.Core.Modules;
using Odumbrata.Data.Runtime;
using Odumbrata.Data.Static;

namespace Odumbrata.Behaviour.Rooms
{
    public sealed class DoorsInitializationModule : BaseRoomModule,
        IConfigurableModule<DoorsInitializationModuleConfig>,
        IRuntimeDataModule<DoorRuntimeData[]>
    {
        private readonly ConfigurableModule<DoorsInitializationModuleConfig> _configurableModule = new();
        private readonly RuntimeDataModule<DoorRuntimeData[]> _runtimeDataModule = new();
        public DoorsInitializationModuleConfig Config => _configurableModule.Config;
        public DoorRuntimeData[] RuntimeData => _runtimeDataModule.RuntimeData;

        public override async Task Initialize(Type context, EventSystem events)
        {
            await base.Initialize(context, events);

            for (var i = 0; i < Config.Doors.Length; i++)
            {
                var door = Config.Doors[i];
                var data = RuntimeData[i];
                door.Initialize(data, events);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _configurableModule.Dispose();
            _runtimeDataModule.Dispose();
        }

        public void SetConfiguration(DoorsInitializationModuleConfig config)
        {
            _configurableModule.SetConfiguration(config);
        }

        public void SetRuntime(DoorRuntimeData[] runtime)
        {
            _runtimeDataModule.SetRuntime(runtime);
        }
    }
}