using Odumbrata.Behaviour.Player;
using UnityEngine;

namespace Odumbrata.Services.Player
{
    [CreateAssetMenu(menuName = "Configs/Services/Player", fileName = "New Player Service Settings", order = 0)]
    public class PlayerServiceSettings : ScriptableObject
    {
        [SerializeField] private PlayerBehaviour _prefab;

        public PlayerBehaviour Prefab => _prefab;
    }
}