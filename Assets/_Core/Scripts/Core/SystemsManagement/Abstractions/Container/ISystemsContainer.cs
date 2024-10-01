namespace Odumbrata.Core.Container
{
    public interface ISystemsContainer : ISystemsContainerReadonly
    {
        public void Add<TSystem>(params TSystem[] systems) where TSystem : BaseSystem;
        public void Remove<TSystem>(params TSystem[] systems) where TSystem : BaseSystem;
    }
}