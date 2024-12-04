namespace Odumbrata.Core.Saves
{
    public interface ISavesSystem
    {
        TData Load<TData>(string key, TData defaultValue);
        void Save<TData>(TData data, string key);
    }
}