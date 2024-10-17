using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using Better.Services.Runtime;
using Cinemachine;
using DG.Tweening;
using Odumbrata.Extensions;
using UnityEngine;

namespace Odumbrata.Services.Camera
{
    public sealed class CameraService : MonoService
    {
        [SerializeField] private float _maxOrthographicSize;
        [SerializeField] private float _minOrthographicSize;

        [SerializeField] private CinemachineBrain _brain;
        [SerializeField] private CinemachineVirtualCameraBase[] _virtualCameras;
        public CinemachineVirtualCamera Current => Brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        public CinemachineBrain Brain => _brain;
        public UnityEngine.Camera BrainMainCamera { get; private set; }

        public float OrthographicSize
        {
            get => Current.m_Lens.OrthographicSize;

            set => Current.m_Lens.OrthographicSize = value;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            BrainMainCamera = _brain.GetComponent<UnityEngine.Camera>();

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


            var follow = Current.Follow;
            var lookAt = Current.LookAt;

            cameraToActivate.Follow = follow;
            cameraToActivate.LookAt = lookAt;
        }

        public void SetTarget(ICameraTarget target)
        {
            if (Current == null)
            {
                return;
            }

            Current.SetTarget(target);
        }

        public Task Zoom(float to, float durationOrSpeed, bool speedBased = false)
        {
            var processedValue = GetProcessedOrthographicSize(to);

            if (Current == null)
            {
                return Task.CompletedTask;
            }

            var tween = DOTween
                .To(() => OrthographicSize, y => OrthographicSize = y, processedValue, durationOrSpeed)
                .SetSpeedBased(speedBased);

            return tween.AsTask(DestroyCancellationToken);
        }

        public void ZoomImmediately(float to)
        {
            var processedValue = GetProcessedOrthographicSize(to);

            if (Current == null)
            {
                return;
            }

            OrthographicSize = processedValue;
        }

        private float GetProcessedOrthographicSize(float to)
        {
            return Mathf.Clamp(to, _minOrthographicSize, _maxOrthographicSize);
        }
    }
}