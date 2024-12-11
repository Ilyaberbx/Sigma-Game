using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Odumbrata.Global.Services;
using Odumbrata.Global.Services.SceneManagement;
using Odumbrata.Global.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Odumbrata
{
    public sealed class GameBootstrapper : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async void Initialize()
        {
            Debug.Log("Bootstrapping...");

            if (SceneManager.GetActiveScene().name == SceneConstants.CoreScene)
            {
                return;
            }

            await SceneManager.LoadSceneAsync(SceneConstants.CoreScene, LoadSceneMode.Single);
        }

        private async void Start()
        {
            var gameService = ServiceLocator.Get<GameService>();
            gameService.Run();
            var initializationState = new GameInitializeState();
            var gameplayState = new GameplayState();
            await gameService.ChangeStateAsync(initializationState);
            await gameService.ChangeStateAsync(gameplayState);
        }
    }
}