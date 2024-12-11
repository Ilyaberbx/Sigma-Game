using System;
using UnityEngine;

namespace Odumbrata.Data.Static
{
    [Serializable]
    public sealed class PlayerInitializationModuleConfig
    {
        [SerializeField] private Transform _playerDefaultPoint;
        [SerializeField] private Transform _root;

        public Vector3 DefaultPosition => _playerDefaultPoint.position;

        public Transform Root => _root;
    }
}