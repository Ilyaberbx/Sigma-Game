using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.Modules;
using Odumbrata.Global.States;

namespace Odumbrata.Global.Services
{
    public sealed class GameService : PocoService, IStateMachine<BaseGameState>
    {
        public event Action<BaseGameState> StateChanged;
        private IStateMachine<BaseGameState> _stateMachine;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _stateMachine = new StateMachine<BaseGameState>();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public bool IsRunning => _stateMachine.IsRunning;
        public BaseGameState CurrentState => _stateMachine.CurrentState;
        public bool InTransition => _stateMachine.InTransition;
        public Task TransitionTask => _stateMachine.TransitionTask;

        public void Run()
        {
            _stateMachine.Run();
        }

        public bool InState<T>() where T : BaseGameState
        {
            return _stateMachine.InState<T>();
        }

        public Task ChangeStateAsync(BaseGameState state, CancellationToken cancellationToken = new CancellationToken())
        {
            return _stateMachine.ChangeStateAsync(state, cancellationToken);
        }

        public void Stop()
        {
            _stateMachine.Stop();
        }

        public bool TryAddModule<TModule>(TModule module) where TModule : Module<BaseGameState>
        {
            return _stateMachine.TryAddModule(module);
        }

        public bool HasModule(Module<BaseGameState> module)
        {
            return _stateMachine.HasModule(module);
        }

        public bool HasModule<TModule>() where TModule : Module<BaseGameState>
        {
            return _stateMachine.HasModule<TModule>();
        }

        public bool TryGetModule<TModule>(out TModule module) where TModule : Module<BaseGameState>
        {
            return _stateMachine.TryGetModule(out module);
        }

        public bool RemoveModule(Module<BaseGameState> module)
        {
            return _stateMachine.RemoveModule(module);
        }
    }
}