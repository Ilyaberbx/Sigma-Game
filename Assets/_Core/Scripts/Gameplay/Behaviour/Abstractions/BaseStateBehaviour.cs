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

        protected StateMachine<TBehaviourState> StateMachine { get; } = new StateMachine<TBehaviourState>();

        protected override void Start()
        {
            base.Start();

            InitializeModules(StateMachine);

            StateMachine.Run();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            DisposeModules(StateMachine);
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

            return StateMachine.ChangeStateAsync(state, destroyCancellationToken);
        }

        protected Task SetState<TState>(TState state) where TState : TBehaviourState
        {
            return StateMachine.ChangeStateAsync(state, destroyCancellationToken);
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