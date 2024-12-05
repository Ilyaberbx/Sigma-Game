using Odumbrata.Core.Commons.Observable;

namespace Odumbrata.Core.Saves
{
    public sealed class PrefsSavesSystem : ISavesSystem
    {
        public TData Load<TData>(string key, TData defaultValue) where TData : Observable
        {
            return defaultValue;
        }

        public void Save<TData>(TData data, string key) where TData : Observable
        {
        }
    }
}