namespace Odumbrata.Core.Saves
{
    public sealed class PrefsSavesSystem : ISavesSystem
    {
        public TData Load<TData>(string key, TData defaultValue)
        {
            TData result = default(TData);

            return result;
        }

        public void Save<TData>(TData data, string key)
        {
        }
    }
}