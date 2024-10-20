using System.Threading.Tasks;
using DG.Tweening;
using Odumbrata.Extensions;
using UnityEngine;

namespace Odumbrata.Behaviour.Common.Door
{
    public enum Side
    {
        Front,
        Back
    }

    public class CommonDoorBehaviour : BaseDoorBehaviour
    {
        [SerializeField] private bool _leftOpenedAfterTransition;
        [SerializeField] private float _transitionDuration;
        [SerializeField] private Ease _transitionEase;
        [SerializeField] private Vector3 _openedRotation;
        [SerializeField] private Vector3 _closedRotation;
        [SerializeField] private Transform _frontInteractionPoint;
        [SerializeField] private Transform _backInteractionPoint;

        private bool _isOpened;
        private bool _inProgress;
        public override bool LeftOpenedAfterTransition => _leftOpenedAfterTransition;
        public override bool IsOpened => _isOpened;
        public override bool InProgress => _inProgress;

        public override Task Open()
        {
            return Rotate(_openedRotation)
                .OnComplete(OnOpened)
                .AsTask(destroyCancellationToken);
        }

        public override Vector3 GetInteractionPosition(Side side)
        {
            return side == Side.Front ? _frontInteractionPoint.position : _backInteractionPoint.position;
        }

        public override Task Close()
        {
            return Rotate(_closedRotation)
                .OnComplete(OnClosed)
                .AsTask(destroyCancellationToken);
        }

        private Tween Rotate(Vector3 to)
        {
            _inProgress = true;

            return Root
                .DORotate(to, _transitionDuration)
                .SetEase(_transitionEase);
        }

        private void OnOpened()
        {
            _inProgress = false;
            _isOpened = true;
        }

        private void OnClosed()
        {
            _inProgress = false;
            _isOpened = false;
        }
    }
}