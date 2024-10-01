using System.Collections.Generic;
using Better.Attributes.Runtime.Select;
using Odumbrata.Systems;
using Odumbrata.Systems.Stats;
using UnityEngine;

namespace Odumbrata.Databases
{
    [CreateAssetMenu(fileName = "Rename Me", menuName = "Databases/Stats", order = 0)]
    public class StatsDatabase : ScriptableObject
    {
        [SerializeReference, Select] private IStat[] _stats;
        public IReadOnlyList<IStat> Stats => _stats;
    }
}