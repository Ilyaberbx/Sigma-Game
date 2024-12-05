using Odumbrata.Behaviour.Levels;
using Odumbrata.Data.Runtime;
using UnityEngine;

namespace Odumbrata.Services.Levels
{
    public class LevelsServiceSettings : ScriptableObject
    {
        [SerializeField] private Transform _root;
        [SerializeField] private BaseLevelBehaviour[] _prefabs;
        [SerializeField] private LevelsData _defaultData;

        public BaseLevelBehaviour[] Prefabs => _prefabs;
        public Transform Root => _root;

        public LevelsData DefaultData => _defaultData;
    }
}