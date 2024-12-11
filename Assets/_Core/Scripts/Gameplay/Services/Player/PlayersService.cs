using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Odumbrata.Behaviour.Player;
using UnityEngine;

namespace Odumbrata.Services.Player
{
    [Serializable]
    public sealed class PlayersService : PocoService<PlayerServiceSettings>
    {
        private PlayerFactory _factory;

        public PlayerBehaviour Player { get; private set; }
        public bool IsPlayerExists => Player != null;

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);
            _factory = new PlayerFactory(Settings.Prefab);
        }

        public async Task AddPlayer(Vector3 at, Quaternion rotation, Transform parent)
        {
            if (Player != null)
            {
                _factory.Destroy(Player);
            }

            var task = _factory.Create(at, rotation, parent);
            var player = await task;
            Player = player;
        }

        public void Clear()
        {
            if (Player != null)
            {
                _factory.Destroy(Player);
            }
        }
    }
}