namespace Odumbrata.Core.Systems.Movement
{
    public class MoveSystem : ISystem
    {
        private IBrain _brain;

        public void Initialize(IBrain brain)
        {
            _brain = brain;
        }
        
    }
}