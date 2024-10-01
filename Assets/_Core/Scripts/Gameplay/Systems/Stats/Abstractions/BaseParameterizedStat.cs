using System;
using UnityEngine;

namespace Odumbrata.Systems.Stats
{
    [Serializable]
    public abstract class BaseParameterizedStat<TValue> : IStat
    {
        [SerializeField] private TValue _value;

        public TValue Value => _value;
    }

    [Serializable]
    public abstract class BaseParameterizedStat<TValue1, TValue2> : IStat
    {
        [SerializeField] private TValue1 _firstValue;
        [SerializeField] private TValue2 _secondValue;
        public TValue1 FirstValue => _firstValue;
        public TValue2 SecondValue => _secondValue;
    }

    [Serializable]
    public abstract class BaseParameterizedStat<TValue1, TValue2, TValue3> : IStat
    {
        [SerializeField] private TValue1 _firstValue;
        [SerializeField] private TValue2 _secondValue;
        [SerializeField] private TValue3 _thirdValue;
        public TValue1 FirstValue => _firstValue;
        public TValue2 SecondValue => _secondValue;
        public TValue3 ThirdValue => _thirdValue;
    }
}