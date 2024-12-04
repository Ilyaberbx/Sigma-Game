using System;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Odumbrata.Behaviour.Levels;
using Odumbrata.Behaviour.Player;
using Odumbrata.Services.Camera;
using UnityEngine;

namespace Odumbrata
{
    public class TestBootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviour _player;
        [SerializeField] private DwellingLevelBehaviour _level;
        
        private CameraService _cameraService;
        private float _prevSize;

        private void Start()
        {
            _cameraService = ServiceLocator.Get<CameraService>();
            _cameraService.SetActive(0);
            _cameraService.SetTarget(_player);
            _level.Enter().Forget();
        }

        private void OnDestroy()
        {
            _level.Exit().Forget();
        }
    }
}