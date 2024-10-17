using Better.Locators.Runtime;
using Odumbrata.Global.Services;
using Odumbrata.Services.Camera;
using UnityEngine;

namespace Odumbrata.Behaviour.Common
{
    public class ScrollCameraBehaviour : BaseBehaviour, IUpdatable
    {
        [SerializeField, Range(0.1f, 2f)] private float _scrollStep;

        private UpdateService _updateService;
        private CameraService _cameraService;

        private void Start()
        {
            _updateService = ServiceLocator.Get<UpdateService>();
            _cameraService = ServiceLocator.Get<CameraService>();

            _updateService.Add(this);
        }

        private void OnDestroy()
        {
            _updateService.Remove(this);
        }

        public void Tick(float deltaTime)
        {
            var scrollDelta = Input.mouseScrollDelta;

            if (scrollDelta == Vector2.zero)
            {
                return;
            }

            var scrollValue = scrollDelta.y;
            var prevOrthographicSize = _cameraService.OrthographicSize;
            var newOrthographicSize = prevOrthographicSize - scrollValue * _scrollStep;

            _cameraService.ZoomImmediately(newOrthographicSize);
        }
    }
}