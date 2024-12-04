using System;
using Better.Locators.Runtime;
using Odumbrata.Core.Container;
using Odumbrata.Services.Input;
using Odumbrata.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Components.Brains
{
    [Serializable]
    public class InputBrainSystem : BaseBrainSystem
    {
        [SerializeField] private NavMeshAgent _agent;
        private InputService _inputService;

        public override void Initialize(ISystemsContainerReadonly container)
        {
            base.Initialize(container);

            _inputService = ServiceLocator.Get<InputService>();

            _inputService.Subscribe(0, KeyInput.Down, OnLeftMouseClicked);
        }

        public override void Dispose()
        {
            base.Dispose();

            _inputService.Unsubscribe(0, KeyInput.Down, OnLeftMouseClicked);
        }


        private void OnLeftMouseClicked()
        {
            var path = new NavMeshPath();

#if UNITY_EDITOR
            DrawPath(Path);
#endif
            var success = PhysicsHelper.TryRaycastScreenPoint(out var info);

            if (!success) return;

            if (!_agent.CalculatePath(info.point, path)) return;

            Path = path;

            if (Path.status == NavMeshPathStatus.PathInvalid)
            {
                InformPathInvalid();
                return;
            }

            InformPathValid(path);
        }

#if UNITY_EDITOR

        private void DrawPath(NavMeshPath path)
        {
            if (Path == null) return;

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
        }
#endif
    }
}