namespace Odumbrata.Core
{
    public interface ISubscriptionHandler<in TElement>
    {
        public void Subscribe(TElement element);
        public void Unsubscribe(TElement element);
    }
}