using Odumbrata.Behaviour.Levels.Modules;

namespace Odumbrata.Core.Modules.Management
{
    public interface IModuleContainer<in TDerivedModule> where TDerivedModule :
        BaseEntityModule
    {
        void AddModule<TModule>(TModule module) where TModule : TDerivedModule;
        void RemoveModule<TModule>() where TModule : TDerivedModule;
        TModule GetModule<TModule>() where TModule : TDerivedModule;
        void Clear();
    }
}