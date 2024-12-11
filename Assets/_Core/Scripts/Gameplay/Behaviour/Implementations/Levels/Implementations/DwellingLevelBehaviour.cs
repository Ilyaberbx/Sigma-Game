using System.Threading.Tasks;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Core.EventSystem;
using Odumbrata.Data.Static;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels
{
    public sealed class DwellingLevelBehaviour : BaseLevelBehaviour
    {
        [SerializeField] private InteractionModuleConfig _interactionModuleConfig;
        [SerializeField] private RoomsInitializationModuleConfig _roomsInitializationModuleConfig;
        [SerializeField] private PlayerInitializationModuleConfig _playerInitializationModuleConfig;


        public override async Task Enter(EventSystem events)
        {
            await base.Enter(events);

            var interactionModule =
                await Factory.CreateWithConfiguration<InteractionModule, InteractionModuleConfig>(
                    _interactionModuleConfig);
            var roomsInitializationModule =
                await Factory.CreateWithConfiguration<RoomsInitializationModule, RoomsInitializationModuleConfig>(
                    _roomsInitializationModuleConfig);
            var playerInitializationModule =
                await Factory.CreateWithConfiguration<PlayerInitializationModule, PlayerInitializationModuleConfig>(
                    _playerInitializationModuleConfig);
            var cameraInitializationModule = await Factory.Create<CameraInitializationModule>();

            AddModule(roomsInitializationModule);
            AddModule(playerInitializationModule);
            AddModule(cameraInitializationModule);
            AddModule(interactionModule);
        }

        public override async Task Exit()
        {
            await base.Exit();

            RemoveModule<InteractionModule>();
            RemoveModule<Modules.RoomsInitializationModule>();
        }
    }
}