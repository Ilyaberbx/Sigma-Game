using System;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Odumbrata.Behaviour.Player;
using Odumbrata.Core.EventSystem;
using Odumbrata.Data.Static;
using Odumbrata.Global.Services;
using Odumbrata.Services.Input;
using Odumbrata.Utils;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels.Modules
{
    public interface IInteractable
    {
        public Outline Outline { get; }
        Task Interact(InteractionData data);
    }

    public class InteractionData
    {
        public PlayerBehaviour Player { get; }

        public InteractionData(PlayerBehaviour player)
        {
            Player = player;
        }
    }

    [Serializable]
    public sealed class InteractionModule : BaseLevelModule<InteractionModuleConfig>, IUpdatable
    {
        private InputService _inputService;
        private UpdateService _updateService;

        private IInteractable _markedForInteraction;
        private IInteractable _currentInInteraction;

        public override void Initialize(Type context, EventSystem events)
        {
            base.Initialize(context, events);

            _inputService = ServiceLocator.Get<InputService>();
            _updateService = ServiceLocator.Get<UpdateService>();

            _inputService.Subscribe(0, KeyInput.Down, OnLeftMouseClicked);
            _updateService.Add(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            _inputService.Unsubscribe(0, KeyInput.Down, OnLeftMouseClicked);
            _updateService.Remove(this);
        }

        public void Tick(float deltaTime)
        {
            if (HasActiveInteraction()) return;

            if (HasMarkedForInteraction())
            {
                ClearInteraction(_markedForInteraction);
                _markedForInteraction = null;
            }

            if (TryMarkInteractable())
            {
                SetOutline(_markedForInteraction, Config.PreInteractionOutlineWidth, Config.PreInteractionOutlineColor);
            }
        }

        private async void OnLeftMouseClicked()
        {
            if (!HasMarkedForInteraction()) return;

            StartInteraction(_markedForInteraction);
            await InteractWithCurrent();
            ClearInteraction(_currentInInteraction);
            _currentInInteraction = null;
        }

        private bool HasActiveInteraction() => _currentInInteraction != null;

        private bool HasMarkedForInteraction() => _markedForInteraction != null;

        private bool TryMarkInteractable()
        {
            if (!PhysicsHelper.TryRaycastScreenPoint<IInteractable>(out var interactable))
                return false;

            _markedForInteraction = interactable;
            return true;
        }

        private void SetOutline(IInteractable interactable, float width, Color color)
        {
            interactable.Outline.OutlineWidth = width;
            interactable.Outline.OutlineColor = color;
        }

        private void ClearInteraction(IInteractable interactable)
        {
            SetOutline(interactable, 0, Color.white);
        }

        private void StartInteraction(IInteractable interactable)
        {
            _currentInInteraction = interactable;
            _markedForInteraction = null;

            SetOutline(_currentInInteraction, Config.InteractionOutlineWidth, Config.InteractionOutlineColor);
        }

        private async Task InteractWithCurrent()
        {
            var data = new InteractionData(Config.Player);
            await _currentInInteraction.Interact(data);
        }
    }
}