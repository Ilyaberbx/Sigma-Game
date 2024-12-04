using System;
using System.Collections.Generic;
using System.Linq;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Utility;
using Odumbrata.Core;
using UnityEngine;

namespace Odumbrata.Components.Stats
{
    [Serializable]
    public sealed class StatsSystem : BaseSystem
    {
        [SerializeReference, Select(typeof(IStat))]
        private List<IStat> _stats;

        public bool TryGet<TStat>(Type type, out TStat result) where TStat : class, IStat
        {
            result = null;

            if (type != typeof(TStat))
            {
                DebugUtility.LogException<InvalidCastException>();
                return false;
            }

            var potentialStat = _stats.FirstOrDefault(temp => temp.GetType() == type);

            if (potentialStat == default)
            {
                return false;
            }

            result = potentialStat as TStat;
            return true;
        }

        public bool TryGet<TStat>(out TStat result) where TStat : class, IStat
        {
            return TryGet(typeof(TStat), out result);
        }
    }
}