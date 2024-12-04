using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Data.Static;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels
{
    public sealed class DwellingLevelBehaviour : BaseLevelBehaviour
    {
        [SerializeField] private InteractionModuleConfig _interactionModuleConfig;
        [SerializeField] private RoomsCoreModuleConfig _roomsCoreModuleConfig;


        public override async Task Enter()
        {
            await base.Enter();

            var interactionModule = Factory.CreateWithConfiguration<InteractionModule, InteractionModuleConfig>(_interactionModuleConfig);
            var roomsCoreModule = Factory.CreateWithConfiguration<RoomsCoreModule, RoomsCoreModuleConfig>(_roomsCoreModuleConfig);
            AddModule(interactionModule);
            AddModule(roomsCoreModule);
        }

        public override async Task Exit()
        {
            await base.Exit();
            
            RemoveModule<InteractionModule>();
            RemoveModule<RoomsCoreModule>();
        }
    }
}