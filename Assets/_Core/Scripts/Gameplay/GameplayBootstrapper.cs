using System;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Odumbrata.Services;
using Odumbrata.Services.Levels;
using Odumbrata.States;
using UnityEngine;

namespace Odumbrata
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        private GameplayService _gameplayService;
        private LevelsService _levelsService;

        private void Start()
        {
            _gameplayService = ServiceLocator.Get<GameplayService>();
            _levelsService = ServiceLocator.Get<LevelsService>();
            _gameplayService.Run();
            _gameplayService.ChangeStateAsync(new LevelGameplayState()).Forget();
        }
    }
}