using System;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Gameplay.Systems
{
    //TODO: Make input service friendly
    [Serializable]
    public class InputBrain : IBrain
    {
        [SerializeField] private Camera _camera;

        private NavMeshPath _path;

        public bool TryGetPath(NavMeshAgent agent, out NavMeshPath path)
        {
            path = new NavMeshPath();

            DrawPath(_path);
            
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
                    
                    return true;
                }
            }

            return false;
        }


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
    }
}