using Odumbrata.Core.Commons.Observable;

namespace Odumbrata.Core.Saves
{
    public interface ISavesSystem
    {
        TData Load<TData>(string key, TData defaultValue) where TData : Observable;
        void Save<TData>(TData data, string key) where TData : Observable;
    }
}