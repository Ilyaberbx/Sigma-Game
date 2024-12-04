using System;
using Odumbrata.Behaviour.Common.Door;
using UnityEngine;

namespace Odumbrata.Data.Static
{
    [Serializable]
    public sealed class DoorsInitializationModuleConfig
    {
        [SerializeField] private BaseDoorBehaviour[] _doors;

        public BaseDoorBehaviour[] Doors => _doors;
    }
}