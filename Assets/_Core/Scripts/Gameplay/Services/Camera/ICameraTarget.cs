using UnityEngine;

namespace Odumbrata.Services.Camera
{
    public interface ICameraTarget
    {
        Transform Follow { get; }

        Transform LookAt { get; }
    }
}