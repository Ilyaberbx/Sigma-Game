using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Odumbrata.Core.Commons.Observable;
using Odumbrata.Core.Saves;

namespace Odumbrata.Global.Services.User
{
    public sealed class UserService : PocoService, ISavesSystem
    {
        private readonly Dictionary<string, Observable> _cachedDataMap = new();
        private ISavesSystem _savesSystem;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _savesSystem = new PrefsSavesSystem();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public TData Load<TData>(string key, TData defaultValue) where TData : Observable
        {
            if (!_cachedDataMap.TryGetValue(key, out var result))
            {
                var loadedData = _savesSystem.Load(key, defaultValue);
                _cachedDataMap.Add(key, loadedData);
                return loadedData;
            }

            if (result is TData data)
            {
                return data;
            }

            return null;
        }

        public void Save<TData>(TData data, string key) where TData : Observable
        {
            _savesSystem.Save(data, key);
        }
    }
}