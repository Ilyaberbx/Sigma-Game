using System;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Odumbrata.Core.EventSystem;
using Odumbrata.Core.Modules;
using Odumbrata.Data.Static;
using Odumbrata.Services.Player;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels.Modules
{
    public sealed class PlayerInitializationModule : BaseLevelModule,
        IConfigurableModule<PlayerInitializationModuleConfig>
    {
        private PlayersService _playersService;
        private readonly ConfigurableModule<PlayerInitializationModuleConfig> _configurableModule = new();

        public PlayerInitializationModuleConfig Config => _configurableModule.Config;

        public override async Task Initialize(Type context, EventSystem events)
        {
            await base.Initialize(context, events);

            _playersService = ServiceLocator.Get<PlayersService>();
            await _playersService.AddPlayer(Config.DefaultPosition, Quaternion.identity, Config.Root);
        }

        public override void Dispose()
        {
            base.Dispose();

            _playersService.Clear();
        }

        public void SetConfiguration(PlayerInitializationModuleConfig config)
        {
            _configurableModule.SetConfiguration(config);
        }
    }
}