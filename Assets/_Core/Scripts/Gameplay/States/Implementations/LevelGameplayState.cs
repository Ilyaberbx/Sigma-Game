using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Odumbrata.Services.Levels;

namespace Odumbrata.States
{
    public sealed class LevelGameplayState : BaseGameplayState
    {
        private LevelsService _levelsService;

        public override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);
            _levelsService = ServiceLocator.Get<LevelsService>();
            var index = _levelsService.CurrentLevelIndex;
            await _levelsService.OpenLevel(index);
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            await base.ExitAsync(token);
            await _levelsService.Clear();
        }
    }
}