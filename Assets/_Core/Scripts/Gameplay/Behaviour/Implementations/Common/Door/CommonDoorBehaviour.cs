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

        public override bool LeftOpenedAfterTransition => _leftOpenedAfterTransition;
        public override Vector3 FrontInteractionPosition => _frontInteractionPoint.position;
        public override Vector3 BackInteractionPosition => _backInteractionPoint.position;

        public override Task Open()
        {
            return Rotate(_openedRotation, _transitionDuration)
                .OnComplete(OnOpened)
                .AsTask(destroyCancellationToken);
        }

        public override Task Close()
        {
            return Rotate(_closedRotation, _transitionDuration)
                .OnComplete(OnClosed)
                .AsTask(destroyCancellationToken);
        }

        public override void CloseImmediately()
        {
            Rotate(_closedRotation, 0f);
        }

        public override void OpenImmediately()
        {
            Rotate(_openedRotation, 0f);
        }

        private Tween Rotate(Vector3 to, float duration)
        {
            return Root
                .DORotate(to, duration)
                .SetEase(_transitionEase);
        }

        private void OnOpened()
        {
            RuntimeData.IsOpen = true;
        }

        private void OnClosed()
        {
            RuntimeData.IsOpen = false;
        }
    }
}