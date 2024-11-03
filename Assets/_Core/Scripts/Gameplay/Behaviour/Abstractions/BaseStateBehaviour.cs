using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.StateMachine.Runtime;
using Odumbrata.Core.Modules;
using Odumbrata.Entity;
using UnityEngine;

namespace Odumbrata.Behaviour
{
    public abstract class BaseStateBehaviour<TBehaviourState> : BaseBehaviourWithSystems
        where TBehaviourState : BaseEntityState
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

            return SetStateAsync(state);
        }


        protected Task SetStateAsync<TState>(TState state) where TState : TBehaviourState
        {
            SetupState(state);

            return StateMachine.ChangeStateAsync(state, destroyCancellationToken);
        }

        protected Task SetStateAsync<TState, TData>(TState state, TData data) where TState : TBehaviourState
        {
            SetupState(state, data);

            return StateMachine.ChangeStateAsync(state, destroyCancellationToken);
        }

        private void SetupState(TBehaviourState state)
        {
            SetupState<object>(state, null);
        }

        private void SetupState<TData>(TBehaviourState state, TData data)
        {
            if (state is BaseBehaviourState<TData> processedState)
            {
                processedState.Initialize(SystemsContainer, data);
            }
        }

        #endregion States
    }
}