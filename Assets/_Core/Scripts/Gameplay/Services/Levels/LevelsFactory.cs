using Odumbrata.Behaviour.Levels;
using UnityEngine;

namespace Odumbrata.Services.Levels
{
    public sealed class LevelsFactory
    {
        private readonly BaseLevelBehaviour[] _prefabs;
        private readonly Transform _root;

        public LevelsFactory(BaseLevelBehaviour[] prefabs, Transform root)
        {
            _prefabs = prefabs;
            _root = root;
        }

        public BaseLevelBehaviour Create(int index, Vector3 at = default)
        {
            if (_prefabs.Length <= index)
            {
                return null;
            }

            var prefab = _prefabs[index];
            return Object.Instantiate(prefab, at, Quaternion.identity, _root);
        }
    }
}