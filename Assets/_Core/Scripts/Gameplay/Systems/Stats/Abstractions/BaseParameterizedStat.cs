using System;
using UnityEngine;

namespace Odumbrata.Gameplay.Systems
{
    [Serializable]
    public abstract class BaseParameterizedStat<TValue> : IStat
    {
        [SerializeField] private TValue _value;

        public TValue Value => _value;
    }
}