using Better.Locators.Runtime;
using Odumbrata.Services.Camera;
using UnityEngine;

namespace Odumbrata.Utils
{
    public static class PhysicsHelper
    {
        private static ServiceProperty<CameraService> CameraProperty { get; } = new();

        public static bool TryRaycastScreenPoint(out RaycastHit info)
        {
            info = default;
            if (!CameraProperty.IsRegistered)
            {
                return false;
            }

            var cameraService = CameraProperty.CachedService;

            var camera = cameraService.MainCamera;
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            return Physics.Raycast(ray, out info);
        }

        public static bool TryRaycastScreenPoint<TComponent>(out TComponent component)
        {
            if (TryRaycastScreenPoint(out var info))
            {
                return info.transform.TryGetComponent(out component);
            }

            component = default;
            return false;
        }
    }
}