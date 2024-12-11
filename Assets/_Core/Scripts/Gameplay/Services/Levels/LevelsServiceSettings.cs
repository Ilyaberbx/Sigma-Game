using Odumbrata.Behaviour.Levels;
using Odumbrata.Data.Runtime;
using UnityEngine;

namespace Odumbrata.Services.Levels
{
    [CreateAssetMenu(menuName = "Configs/Services/Levels", fileName = "New Levels Service Settings", order = 0)]
    public class LevelsServiceSettings : ScriptableObject
    {
        [SerializeField] private BaseLevelBehaviour[] _prefabs;
        [SerializeField] private LevelsData _defaultData;

        public BaseLevelBehaviour[] Prefabs => _prefabs;
        public LevelsData DefaultData => _defaultData;
    }
}