using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.StateMachine.Runtime;
using Odumbrata.Core.Modules;
using UnityEngine;

namespace Odumbrata.Behaviour
{
    public abstract class BaseStateBehaviour<TBehaviourState> : BaseBehaviourWithSystems
        where TBehaviourState : BaseBehaviourState
    {
        [SerializeField] private bool _logStateChanges;

        #region States

        private readonly StateMachine<TBehaviourState> _stateMachine = new StateMachine<TBehaviourState>();

        protected override void Start()
        {
            base.Start();

            InitializeModules(_stateMachine);

            _stateMachine.Run();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            DisposeModules(_stateMachine);
        }

        protected virtual void InitializeModules(StateMachine<TBehaviourState> stateMachine)
        {
            if (_logStateChanges)
            {
                stateMachine.AddModule<TBehaviourState, LoggerModule<TBehaviourState>>();
            }
        }

        protected virtual void DisposeModules(StateMachine<TBehaviourState> stateMachine)
        {
            if (_logStateChanges)
            {
                stateMachine.RemoveModule<TBehaviourState, LoggerModule<TBehaviourState>>();
            }
        }

        private Task SetStateAsync<TState>() where TState : TBehaviourState, new()
        {
            var state = new TState();

            SetupState(state);

            return _stateMachine.ChangeStateAsync(state, destroyCancellationToken);
        }

        protected Task SetState<TState>(TState state) where TState : TBehaviourState
        {
            return _stateMachine.ChangeStateAsync(state, destroyCancellationToken);
        }

        protected void SetState<TState>() where TState : TBehaviourState, new()
        {
            SetStateAsync<TState>().Forget();
        }

        protected virtual void SetupState(TBehaviourState state)
        {
            state.Initialize(SystemsContainer);
        }

        #endregion States
    }
}