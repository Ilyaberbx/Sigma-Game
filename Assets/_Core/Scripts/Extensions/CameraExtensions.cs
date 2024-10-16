using Cinemachine;
using Odumbrata.Services.Camera;

namespace Odumbrata.Extensions
{
    public static class CameraExtensions
    {
        public static void SetTarget(this CinemachineVirtualCameraBase source, ICameraTarget target)
        {
            source.Follow = target.Follow;
            source.LookAt = target.LookAt;
        }
    }
}