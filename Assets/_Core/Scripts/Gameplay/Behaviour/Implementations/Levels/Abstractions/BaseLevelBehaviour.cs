using System.Threading.Tasks;

namespace Odumbrata.Behaviour.Levels
{
    public abstract class BaseLevelBehaviour : BaseLevelBehaviour<object>
    {
        public abstract Task Enter();

        public sealed override Task Enter(object data)
        {
            return Enter();
        }
    }

    public abstract class BaseLevelBehaviour<TData> : BaseBehaviour
    {
        public abstract Task Enter(TData data);
        public abstract Task Exit();
    }
}