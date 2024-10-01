using UnityEngine;

namespace Odumbrata.Behaviour
{
    public abstract class BaseBehaviour : MonoBehaviour, IBehaviour
    {
        #region Unity World Properties

        public Vector3 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }

        public Vector3 LocalPosition
        {
            get => Transform.localPosition;
            set => Transform.localPosition = value;
        }

        public Vector3 LocalScale
        {
            get => Transform.localScale;
            set => Transform.localScale = value;
        }

        public Vector3 LossyScale => Transform.lossyScale;

        public Quaternion Rotation
        {
            get => Transform.rotation;
            set => Transform.rotation = value;
        }

        public Quaternion LocalRotation
        {
            get => Transform.localRotation;
            set => Transform.localRotation = value;
        }

        public GameObject GameObject => gameObject;
        public Transform Transform { get; private set; }

        #endregion

        #region Unity Callbacks

        protected virtual void Awake()
        {
            Transform = transform;
        }

        #endregion
    }
}