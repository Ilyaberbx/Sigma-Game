using System;
using Better.Locators.Runtime;
using Odumbrata.Core;
using Odumbrata.Core.Container;
using Odumbrata.Global.Services;
using Odumbrata.Utils;
using UnityEngine;

namespace Odumbrata.Features.Interaction
{
    [Serializable]
    public sealed class InteractionSystem : BaseSystem, IUpdatable
    {
        [SerializeField] private Transform _interactor;

        private UpdateService _updateService;
        private IInteractable _currentInteractable;

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
            var success = PhysicsHelper.TryRaycastScreenPoint(out var info);

            if (!success)
            {
                _currentInteractable?.FinishInteraction(_interactor);
                _currentInteractable = null;

                return;
            }

            var target = info.collider.gameObject;

            if (!target.TryGetComponent<IInteractable>(out var interactable)) return;

            _currentInteractable = interactable;
            _currentInteractable.StartInteraction(_interactor);
        }
    }
}