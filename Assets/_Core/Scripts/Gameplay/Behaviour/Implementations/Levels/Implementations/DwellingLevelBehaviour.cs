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

            AddModule<InteractionModule, InteractionModuleConfig>(_interactionModuleConfig);
            AddModule<RoomsCoreModule, RoomsCoreModuleConfig>(_roomsCoreModuleConfig);
        }

        public override Task Exit()
        {
            return base.Exit();
        }
    }
}