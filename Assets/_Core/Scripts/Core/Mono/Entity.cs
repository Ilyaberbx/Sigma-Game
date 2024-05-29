using System;
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
            Initialize();
        }
        
        protected virtual void Initialize()
        {
            _statsSystem = new StatsSystem(_statsDatabase.Stats);
        }
    }
}