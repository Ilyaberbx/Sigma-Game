using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Data.Static;
using UnityEngine;
using RoomsInitializationModule = Odumbrata.Data.Static.RoomsInitializationModule;

namespace Odumbrata.Behaviour.Levels
{
    public sealed class DwellingLevelBehaviour : BaseLevelBehaviour
    {
        [SerializeField] private InteractionModuleConfig _interactionModuleConfig;
        [SerializeField] private RoomsInitializationModule roomsInitializationModule;


        public override async Task Enter()
        {
            await base.Enter();

            var interactionModule = Factory.CreateWithConfiguration<InteractionModule, InteractionModuleConfig>(_interactionModuleConfig);
            var roomsCoreModule = Factory.CreateWithConfiguration<Modules.RoomsInitializationModule, RoomsInitializationModule>(roomsInitializationModule);
            AddModule(interactionModule);
            AddModule(roomsCoreModule);
        }

        public override async Task Exit()
        {
            await base.Exit();
            
            RemoveModule<InteractionModule>();
            RemoveModule<Modules.RoomsInitializationModule>();
        }
    }
}