using System;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Odumbrata.Core.EventSystem;
using Odumbrata.Services.Camera;
using Odumbrata.Services.Player;

namespace Odumbrata.Behaviour.Levels.Modules
{
    public sealed class CameraInitializationModule : BaseLevelModule
    {
        public override async Task Initialize(Type context, EventSystem events)
        {
            await base.Initialize(context, events);
            var playerService = ServiceLocator.Get<PlayersService>();
            var cameraService = ServiceLocator.Get<CameraService>();

            if (!playerService.IsPlayerExists)
            {
                return;
            }

            cameraService.SetActive(0);
            cameraService.SetTarget(playerService.Player);
        }
    }
}