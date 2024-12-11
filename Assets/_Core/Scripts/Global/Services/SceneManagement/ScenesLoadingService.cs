using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Odumbrata.Core.Async;
using Odumbrata.Extensions;
using UnityEngine.SceneManagement;

namespace Odumbrata.Global.Services.SceneManagement
{
    public enum LoadingSceneType
    {
        Additional,
        Single
    }

    public sealed class SceneLoadingData
    {
        public ScenesGroup Group { get; }

        public IProgress<float> Progress { get; }

        public bool ReloadDuplicates { get; }

        public LoadingSceneType Type { get; }

        public SceneLoadingData(ScenesGroup group, IProgress<float> progress, bool reloadDuplicates,
            LoadingSceneType type)
        {
            Group = group;
            Progress = progress;
            ReloadDuplicates = reloadDuplicates;
            Type = type;
        }
    }

    public sealed class ScenesLoadingService : PocoService
    {
        public event Action<string> OnSceneLoaded;
        public event Action<string> OnSceneUnloaded;
        public event Action<string> OnSceneGroupLoaded;
        private ScenesGroup ActiveScenesGroup { get; set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task LoadScenes(SceneLoadingData loadingData)
        {
            ActiveScenesGroup = loadingData.Group;

            await UnloadScenes();
            var sceneCount = SceneManager.sceneCount;
            var loadedScenes = new List<string>();

            for (int i = 0; i < sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i).name;
                loadedScenes.Add(scene);
            }

            var totalScenesToLoad = ActiveScenesGroup.GetScenesCount();
            var operationGroup = new AsyncOperationsGroup(totalScenesToLoad);

            for (int i = 0; i < totalScenesToLoad; i++)
            {
                var data = ActiveScenesGroup.Scenes[i];
                if (loadingData.ReloadDuplicates == false && loadedScenes.Contains(data.Name))
                {
                    continue;
                }

                var operation = SceneManager.LoadSceneAsync(data.Path, LoadSceneMode.Additive);
                operationGroup.Add(operation);
                OnSceneLoaded.SafeInvoke(data.Name);
            }

            while (!operationGroup.IsDone)
            {
                loadingData.Progress?.Report(operationGroup.Progress);
                await Task.Yield();
            }

            ApplyActiveScene();

            OnSceneGroupLoaded.SafeInvoke(ActiveScenesGroup.Name);
        }

        public async Task UnloadScenes()
        {
            var scenesToUnload = new List<string>();
            var activeScene = SceneManager.GetActiveScene().name;
            var sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount - 1; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (!scene.isLoaded)
                {
                    return;
                }

                var sceneName = scene.name;
                if (sceneName.Equals(activeScene) || sceneName == SceneConstants.CoreScene)
                {
                    continue;
                }

                scenesToUnload.Add(sceneName);
            }

            var operations = new AsyncOperationsGroup(scenesToUnload.Count);

            foreach (var sceneToUnload in scenesToUnload)
            {
                var operation = SceneManager.UnloadSceneAsync(sceneToUnload);
                if (operation == null)
                {
                    continue;
                }

                operations.Add(operation);
                OnSceneUnloaded.SafeInvoke(sceneToUnload);
            }

            while (!operations.IsDone)
            {
                await Task.Yield();
            }
        }

        private void ApplyActiveScene()
        {
            var activeName = ActiveScenesGroup.FindSceneNameByType(SceneType.Active);
            var activeScene = SceneManager.GetSceneByName(activeName);
            if (activeScene.IsValid())
            {
                SceneManager.SetActiveScene(activeScene);
            }
        }
    }
}