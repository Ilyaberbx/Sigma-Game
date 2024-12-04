using System;
using Odumbrata.Behaviour.Common.Door;
using UnityEngine;

namespace Odumbrata.Data.Static
{
    [Serializable]
    public sealed class DoorsModuleConfig
    {
        [SerializeField] private float _transitionDuration;
        [SerializeField] private BaseDoorBehaviour[] _doors;

        public BaseDoorBehaviour[] Doors => _doors;
        public float TransitionDuration => _transitionDuration;
    }
}