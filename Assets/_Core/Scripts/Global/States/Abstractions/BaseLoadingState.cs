using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Odumbrata.Core.Operations.Progress;
using Odumbrata.Global.Services.SceneManagement;

namespace Odumbrata.Global.States
{
    public abstract class BaseLoadingState : BaseGameState
    {
        private ScenesLoadingService _scenesLoadingService;
        private ScenesProviderService _scenesProviderService;
        private string _requestedScenes;
        private bool _isLoaded;

        public sealed override async Task EnterAsync(CancellationToken token)
        {
            _scenesProviderService = ServiceLocator.Get<ScenesProviderService>();
            if (!_scenesProviderService.TryGetGroup(GetType(), out var scenes))
            {
                return;
            }

            _isLoaded = false;
            _requestedScenes = scenes.Name;
            _scenesLoadingService = ServiceLocator.Get<ScenesLoadingService>();
            _scenesLoadingService.OnSceneGroupLoaded += OnScenesLoaded;
            var loadingProgress = new LoadingProgress();
            var loadingData = new SceneLoadingData(scenes, loadingProgress, false, LoadingSceneType.Additional);
            await _scenesLoadingService.LoadScenes(loadingData);
        }

        public sealed override Task ExitAsync(CancellationToken token)
        {
            if (_isLoaded)
            {
                return Task.CompletedTask;
            }

            _scenesLoadingService.OnSceneGroupLoaded -= OnScenesLoaded;
            return Task.CompletedTask;
        }

        private void OnScenesLoaded(string scenes)
        {
            if (_requestedScenes != scenes)
            {
                return;
            }

            _isLoaded = true;
            _scenesLoadingService.OnSceneGroupLoaded -= OnScenesLoaded;
        }
    }
}