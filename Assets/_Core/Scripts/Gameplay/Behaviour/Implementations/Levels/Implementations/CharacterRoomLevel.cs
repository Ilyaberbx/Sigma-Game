using System.Threading.Tasks;

namespace Odumbrata.Behaviour.Levels
{
    public class CharacterRoomLevel : BaseLevelBehaviour
    {
        public override Task Enter()
        {
            return Task.CompletedTask;
        }

        public override Task Exit()
        {
            return Task.CompletedTask;
        }
    }
}