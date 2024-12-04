namespace Odumbrata.Core.Modules
{
    public interface IConfigurableModule<TConfig>
    {
        public TConfig Config { get; }
        public void SetConfiguration(TConfig config);
    }
}