using System.Threading.Tasks;
using DG.Tweening;
using Odumbrata.Extensions;
using UnityEngine;

namespace Odumbrata.Behaviour.Common.Door
{
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
        protected override bool LeftOpenedAfterTransition => _leftOpenedAfterTransition;
        protected override bool IsOpened => _isOpened;

        protected override Task Open()
        {
            return Rotate(_openedRotation)
                .OnComplete(OnOpened)
                .AsTask(destroyCancellationToken);
        }

        protected override Task Close()
        {
            return Rotate(_closedRotation)
                .OnComplete(OnClosed)
                .AsTask(destroyCancellationToken);
        }

        protected override Vector3 GetInteractionPosition(IDoorHandler handler)
        {
            var handlerPosition = handler.Position;

            var frontPosition = _frontInteractionPoint.position;
            var backPosition = _backInteractionPoint.position;

            var frontDistance = Vector3.Distance(handlerPosition, frontPosition);
            var backDistance = Vector3.Distance(handlerPosition, backPosition);

            return frontDistance < backDistance ? frontPosition : backPosition;
        }

        private Tween Rotate(Vector3 to)
        {
            return Root
                .DORotate(to, _transitionDuration)
                .SetEase(_transitionEase);
        }

        private void OnOpened()
        {
            _isOpened = true;
        }

        private void OnClosed()
        {
            _isOpened = false;
        }
    }
}