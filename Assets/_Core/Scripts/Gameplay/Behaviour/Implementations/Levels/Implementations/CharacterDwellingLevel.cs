using System;
using System.Threading.Tasks;

namespace Odumbrata.Behaviour.Levels
{
    public class CharacterDwellingLevel : BaseLevelBehaviour<CharacterDwellingData>
    {
        public override Task Enter(CharacterDwellingData data)
        {
            return Task.CompletedTask;
        }

        public override Task Exit()
        {
            return Task.CompletedTask;
        }
    }

    [Serializable]
    public class CharacterDwellingData
    {
    }
}