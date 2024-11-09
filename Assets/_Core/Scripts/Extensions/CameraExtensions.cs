using Cinemachine;
using Odumbrata.Services.Camera;

namespace Odumbrata.Extensions
{
    public static class CameraExtensions
    {
        public static void SetTarget(this ICinemachineCamera source, ICameraTarget target)
        {
            source.Follow = target.CameraFollow;
            source.LookAt = target.CameraLookAt;
        }
    }
}