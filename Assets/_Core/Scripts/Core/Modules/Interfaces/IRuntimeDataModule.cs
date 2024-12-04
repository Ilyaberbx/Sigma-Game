namespace Odumbrata.Core.Modules
{
    public interface IRuntimeDataModule<TRuntimeData>
    {
        public TRuntimeData RuntimeData { get; }
        public void SetRuntime(TRuntimeData runtime);
    }
}