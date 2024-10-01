using UnityEngine;

namespace Odumbrata.Behaviour
{
    public interface IBehaviour
    {
        public Vector3 Position { get; set; }
        public Vector3 LossyScale { get; }
        public Vector3 LocalScale { get; set; }
        public Vector3 LocalPosition { get; set; }
        public Quaternion Rotation { get; set; }
        public Quaternion LocalRotation { get; set; }
        public GameObject GameObject { get; }
        public Transform Transform { get; }
    }
}