using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Odumbrata.Behaviour.Player;
using Odumbrata.Core.EventSystem;
using Odumbrata.Global.Services;
using Odumbrata.Services.Input;
using Odumbrata.Utils;
using UnityEngine;

namespace Odumbrata.Behaviour.Levels.Modules
{
    public interface IInteractable
    {
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
    public sealed class InteractionModule : BaseLevelModule, IUpdatable
    {
        [SerializeField] private PlayerBehaviour _player;

        private InputService _inputService;
        private UpdateService _updateService;

        private IInteractable _markedForInteraction;
        private IInteractable _currentInInteraction;

        public override void Initialize(EventSystem events)
        {
            base.Initialize(events);

            _inputService = ServiceLocator.Get<InputService>();
            _updateService = ServiceLocator.Get<UpdateService>();

            _inputService.Subscribe(0, KeyInput.Down, OnLeftMouseClicked);
            _updateService.Add(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            _updateService.Remove(this);
            _inputService.Unsubscribe(0, KeyInput.Down, OnLeftMouseClicked);
        }

        public void Tick(float deltaTime)
        {
            if (_currentInInteraction != null)
            {
                return;
            }

            var success = PhysicsHelper.TryRaycastScreenPoint<IInteractable>(out var interactable);

            if (!success)
            {
                _markedForInteraction = null;
                return;
            }

            _markedForInteraction = interactable;
        }

        private async void OnLeftMouseClicked()
        {
            if (_currentInInteraction != null) return;
            if (_markedForInteraction == null) return;

            var data = new InteractionData(_player);

            _currentInInteraction = _markedForInteraction;
            await _currentInInteraction.Interact(data);
            _currentInInteraction = null;
        }
    }
}