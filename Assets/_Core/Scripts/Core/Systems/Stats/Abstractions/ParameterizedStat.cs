using System;
using UnityEngine;

namespace Odumbrata.Stats.Abstractions
{
    [Serializable]
    public abstract class ParameterizedStat<TValue> : IStat
    {
        [SerializeField] private TValue _value;

        public TValue Value => _value;
    }
}