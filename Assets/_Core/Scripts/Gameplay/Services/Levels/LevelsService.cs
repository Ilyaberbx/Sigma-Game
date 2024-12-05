using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Odumbrata.Behaviour.Levels;
using Odumbrata.Core.EventSystem;
using Odumbrata.Data.Runtime;
using Odumbrata.Global.Services.User;

namespace Odumbrata.Services.Levels
{
    public sealed class LevelsService : PocoService<LevelsServiceSettings>
    {
        private UserService _userService;
        private LevelsFactory _factory;
        private LevelsData _data;
        private BaseLevelBehaviour _currentLevel;
        private EventSystem _events;
        public int CurrentLevelIndex => _data.Index;

        private int MaxLevelIndex
        {
            get
            {
                if (Settings.Prefabs.IsNullOrEmpty())
                {
                    return -1;
                }

                return Settings.Prefabs.Length - 1;
            }
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _events = new EventSystem();
            _factory = new LevelsFactory(Settings.Prefabs, Settings.Root);
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _userService = ServiceLocator.Get<UserService>();
            _data = _userService.Load(UserKeys.Levels, Settings.DefaultData);

            return Task.CompletedTask;
        }

        public async Task OpenLevel(int index)
        {
            var newLevel = _factory.Create(index);
            if (newLevel == null)
            {
                return;
            }

            if (_currentLevel != null)
            {
                await _currentLevel.Exit();
            }

            _currentLevel = newLevel;
            await _currentLevel.Enter(_events);
        }

        public Task Clear()
        {
            return _currentLevel != null ? _currentLevel.Exit() : Task.CompletedTask;
        }

        public void NextLevel()
        {
            if (_data.Index >= MaxLevelIndex)
            {
                return;
            }

            _data.Index++;
            Save();
        }

        public void PreviousLevel()
        {
            if (_data.Index <= 0)
            {
                return;
            }

            _data.Index--;

            Save();
        }

        private void Save()
        {
            _userService.Save(_data, UserKeys.Levels);
        }
    }
}