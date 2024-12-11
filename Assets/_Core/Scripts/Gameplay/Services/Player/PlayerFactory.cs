using System;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Odumbrata.Services.Player
{
    public sealed class PlayerFactory
    {
        private readonly PlayerBehaviour _prefab;

        public PlayerFactory(PlayerBehaviour prefab)
        {
            _prefab = prefab;
        }

        public Task<PlayerBehaviour> Create(Vector3 at, Quaternion rotation, Transform parent)
        {
            if (_prefab == null)
            {
                var exception = new NullReferenceException("Can not find player prefab");
                return Task.FromException<PlayerBehaviour>(exception);
            }

            var player = Object.Instantiate(_prefab, at, rotation, parent);
            return Task.FromResult(player);
        }

        public void Destroy(PlayerBehaviour player)
        {
            player.Destroy();
        }
    }
}