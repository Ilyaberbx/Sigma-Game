using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utility;

namespace Odumbrata.Stats
{
    public class StatsSystem : ISystem
    {
        private readonly IReadOnlyList<IStat> _stats;
        public StatsSystem(IReadOnlyList<IStat> stats)
        {
            _stats = stats;
        }

        public bool TryGet<TStat>(out TStat result) where TStat : class, IStat
        {
            result = null;

            foreach (var derivedStat in _stats)
            {
                var type = derivedStat.GetType();

                if (type == typeof(TStat))
                {
                    if (derivedStat is TStat stat)
                    {
                        result = stat;
                    }
                    else
                    {
                        DebugUtility.LogException<InvalidCastException>();
                    }
                    
                    return true;
                }
            }

            return false;
        }
    }
}