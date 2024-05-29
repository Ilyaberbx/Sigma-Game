using System.Collections.Generic;
using Better.Attributes.Runtime.Select;
using UnityEngine;

namespace Odumbrata.Stats
{
    [CreateAssetMenu(fileName = "Entity Stats", menuName = "Databases/Stats", order = 0)]
    public class StatsDatabase : ScriptableObject
    {
        [SerializeReference, Select] private IStat[] _stats;
        public IReadOnlyList<IStat> Stats => _stats;
    }
}