using Odumbrata.Stats;
using UnityEngine;

namespace Odumbrata.Mono
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private StatsDatabase _statsDatabase;
        
        private StatsSystem _statsSystem;

        protected StatsSystem Stats => _statsSystem;
        
        private void Awake()
        {
            OnAwake();
        }
        
        protected virtual void OnAwake()
        {
            _statsSystem = new StatsSystem(_statsDatabase.Stats);
        }
    }
}