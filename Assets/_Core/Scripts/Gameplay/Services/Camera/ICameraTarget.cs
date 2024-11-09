using UnityEngine;

namespace Odumbrata.Services.Camera
{
    public interface ICameraTarget
    {
        Transform CameraFollow { get; }

        Transform CameraLookAt { get; }
    }
}