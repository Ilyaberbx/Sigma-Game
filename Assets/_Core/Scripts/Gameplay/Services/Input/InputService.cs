using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Odumbrata.Extensions;
using Odumbrata.Global.Services;
using UnityEngine;

namespace Odumbrata.Services.Input
{
    public enum KeyInput
    {
        Up,
        Down,
        Hold
    }

    public sealed class InputService : PocoService, IUpdatable, IDisposable
    {
        private UpdateService _updateService;

        private readonly Dictionary<Action, KeyCode> _keysUpMap = new();
        private readonly Dictionary<Action, KeyCode> _keysDownMap = new();
        private readonly Dictionary<Action, KeyCode> _keysHoldMap = new();
        private readonly Dictionary<Action, int> _mouseHoldMap = new();
        private readonly Dictionary<Action, int> _mouseUpMap = new();
        private readonly Dictionary<Action, int> _mouseDownMap = new();
        private bool _isLocked;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _updateService = ServiceLocator.Get<UpdateService>();
            _updateService.Add(this);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _updateService.Remove(this);
        }

        public void Tick(float deltaTime)
        {
            if (_isLocked)
            {
                return;
            }

            ProcessInput(_keysUpMap, UnityEngine.Input.GetKeyUp);
            ProcessInput(_keysDownMap, UnityEngine.Input.GetKeyDown);
            ProcessInput(_keysHoldMap, UnityEngine.Input.GetKeyDown);

            ProcessInput(_mouseUpMap, UnityEngine.Input.GetMouseButtonUp);
            ProcessInput(_mouseDownMap, UnityEngine.Input.GetMouseButtonDown);
            ProcessInput(_mouseHoldMap, UnityEngine.Input.GetMouseButton);
        }

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        public void Subscribe(KeyCode key, KeyInput input, Action action)
        {
            var map = GetKeysMap(input);

            if (map.ContainsKey(action))
            {
                Unsubscribe(key, input, action);
            }

            map.Add(action, key);
        }

        public void Unsubscribe(KeyCode key, KeyInput input, Action action)
        {
            var map = GetKeysMap(input);

            if (!map.TryGetValue(action, out var existingKey)) return;

            if (existingKey == key)
            {
                map.Remove(key);
            }
        }

        public void Subscribe(int mouse, KeyInput input, Action action)
        {
            var map = GetMousesMap(input);

            if (map.ContainsKey(action))
            {
                Unsubscribe(mouse, input, action);
            }

            map.Add(action, mouse);
        }

        public void Unsubscribe(int mouse, KeyInput input, Action action)
        {
            var map = GetMousesMap(input);

            if (!map.TryGetValue(action, out var existingMouse)) return;

            if (existingMouse == mouse)
            {
                map.Remove(mouse);
            }
        }

        private void ProcessInput<TInput>(Dictionary<Action, TInput> inputMap, Func<TInput, bool> predicate)
        {
            if (inputMap.IsEmpty())
            {
                return;
            }

            foreach (var inputPair in inputMap)
            {
                if (predicate(inputPair.Value))
                {
                    inputPair.Key.SafeInvoke();
                }
            }
        }

        private Dictionary<Action, KeyCode> GetKeysMap(KeyInput input)
        {
            return input switch
            {
                KeyInput.Up => _keysUpMap,
                KeyInput.Down => _keysDownMap,
                KeyInput.Hold => _keysHoldMap,
                _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
            };
        }

        private Dictionary<Action, int> GetMousesMap(KeyInput input)
        {
            return input switch
            {
                KeyInput.Up => _mouseUpMap,
                KeyInput.Down => _mouseDownMap,
                KeyInput.Hold => _mouseHoldMap,
                _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
            };
        }
    }
}