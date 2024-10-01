using System;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Systems
{
    [Serializable]
    public class InputBrainSystem : BaseBrainSystem
    {
        public event Action OnPathInvalid;

        [SerializeField] private Camera _camera;

        private NavMeshPath _path;

        public override bool TryGetPath(NavMeshAgent agent, out NavMeshPath path)
        {
            path = new NavMeshPath();

#if UNITY_EDITOR
            DrawPath(_path);
#endif
            if (!Input.GetMouseButtonDown(0))
            {
                return false;
            }

            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var info))
            {
                if (agent.CalculatePath(info.point, path))
                {
                    _path = path;

                    if (_path.status == NavMeshPathStatus.PathInvalid)
                    {
                        OnPathInvalid?.Invoke();
                    }

                    return true;
                }
            }

            return false;
        }


#if UNITY_EDITOR
        private void DrawPath(NavMeshPath path)
        {
            if (_path != null) // TODO Preconditions Utils
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