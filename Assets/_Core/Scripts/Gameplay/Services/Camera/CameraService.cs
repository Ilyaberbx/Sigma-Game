using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using Better.Services.Runtime;
using Cinemachine;
using Odumbrata.Extensions;
using UnityEngine;

namespace Odumbrata.Services.Camera
{
    public sealed class CameraService : MonoService
    {
        [SerializeField] private CinemachineVirtualCameraBase[] _virtualCameras;

        public CinemachineVirtualCameraBase Current { get; private set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void SetActive(int index)
        {
            if (_virtualCameras.Length <= index)
            {
                DebugUtility.LogException<IndexOutOfRangeException>();
                return;
            }

            foreach (var virtualCamera in _virtualCameras)
            {
                virtualCamera.Priority = 0;
            }

            var cameraToActivate = _virtualCameras[index];

            cameraToActivate.Priority = 1;

            if (Current != null)
            {
                var follow = Current.Follow;
                var lookAt = Current.LookAt;

                cameraToActivate.Follow = follow;
                cameraToActivate.LookAt = lookAt;
            }

            Current = cameraToActivate;
        }

        public void SetTarget(ICameraTarget target)
        {
            if (Current == null)
            {
                return;
            }

            Current.SetTarget(target);
        }
    }
}