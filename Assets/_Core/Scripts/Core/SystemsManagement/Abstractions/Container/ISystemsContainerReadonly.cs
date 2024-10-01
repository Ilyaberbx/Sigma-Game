namespace Odumbrata.Core.Container
{
    public interface ISystemsContainerReadonly 
    {
        public TSystem GetSystem<TSystem>() where TSystem : BaseSystem;
        public bool TryGetSystem<TSystem>(out TSystem system) where TSystem : BaseSystem;
        public bool HasSystem<TSystem>() where TSystem : BaseSystem;
    }
}