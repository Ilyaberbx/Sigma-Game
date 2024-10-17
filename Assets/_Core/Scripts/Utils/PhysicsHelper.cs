using Better.Locators.Runtime;
using Odumbrata.Services.Camera;
using Odumbrata.Services.Physics;
using UnityEngine;

namespace Odumbrata.Utils
{
    public static class PhysicsHelper
    {
        private static ServiceProperty<PhysicsService> PhysicsProperty { get; } = new();
        private static ServiceProperty<CameraService> CameraProperty { get; } = new();

        public static bool TryRaycastScreenPoint(out RaycastHit info)
        {
            info = default;

            if (!PhysicsProperty.IsRegistered)
            {
                return false;
            }

            if (!CameraProperty.IsRegistered)
            {
                return false;
            }

            var physicsService = PhysicsProperty.CachedService;
            var cameraService = CameraProperty.CachedService;

            var camera = cameraService.BrainMainCamera;
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            return physicsService.TryRaycast(ray, out info);
        }
    }
}