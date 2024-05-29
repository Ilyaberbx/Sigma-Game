namespace Odumbrata.Tick
{
    public interface ITickRegistry<in TTickable> where TTickable : ITickable
    {
        void Subscribe(TTickable tickable);
        void Unsubscribe(TTickable tickable);
    }

    public interface ITickable
    {
        void Tick(float deltaTime);
    }
}