using System;
using Better.Locators.Runtime;
using Odumbrata.Core.Container;
using Odumbrata.Global.Services;
using Odumbrata.Services.Camera;
using Odumbrata.Services.Physics;
using Odumbrata.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Features.Brains
{
    [Serializable]
    public class InputBrainSystem : BaseBrainSystem, IUpdatable
    {
        [SerializeField] private NavMeshAgent _agent;

        private UpdateService _updateService;

        public override void Initialize(ISystemsContainerReadonly container)
        {
            base.Initialize(container);

            _updateService = ServiceLocator.Get<UpdateService>();

            _updateService.Add(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            _updateService.Remove(this);
        }

        public void Tick(float deltaTime)
        {
            var path = new NavMeshPath();

#if UNITY_EDITOR
            DrawPath(Path);
#endif
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

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
            if (Path != null) // TODO Preconditions Utils
            {
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
                }
            }
        }
#endif
    }
}