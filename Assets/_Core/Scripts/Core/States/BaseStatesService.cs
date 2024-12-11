using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.Modules;
using Better.StateMachine.Runtime.States;

namespace Odumbrata.Core.States
{
    public abstract class BaseStatesService<TState> : PocoService, IStateMachine<TState> where TState : BaseState
    {
#pragma warning disable 0067
        public event Action<TState> StateChanged;
        private IStateMachine<TState> _stateMachine;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _stateMachine = new StateMachine<TState>();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public bool IsRunning => _stateMachine.IsRunning;
        public TState CurrentState => _stateMachine.CurrentState;
        public bool InTransition => _stateMachine.InTransition;
        public Task TransitionTask => _stateMachine.TransitionTask;

        public void Run()
        {
            _stateMachine.Run();
        }

        public bool InState<T>() where T : TState
        {
            return _stateMachine.InState<T>();
        }

        public Task ChangeStateAsync(TState state, CancellationToken cancellationToken = new CancellationToken())
        {
            return _stateMachine.ChangeStateAsync(state, cancellationToken);
        }

        public void Stop()
        {
            _stateMachine.Stop();
        }

        public bool TryAddModule<TModule>(TModule module) where TModule : Module<TState>
        {
            return _stateMachine.TryAddModule(module);
        }

        public bool HasModule(Module<TState> module)
        {
            return _stateMachine.HasModule(module);
        }

        public bool HasModule<TModule>() where TModule : Module<TState>
        {
            return _stateMachine.HasModule<TModule>();
        }

        public bool TryGetModule<TModule>(out TModule module) where TModule : Module<TState>
        {
            return _stateMachine.TryGetModule(out module);
        }

        public bool RemoveModule(Module<TState> module)
        {
            return _stateMachine.RemoveModule(module);
        }
    }
}