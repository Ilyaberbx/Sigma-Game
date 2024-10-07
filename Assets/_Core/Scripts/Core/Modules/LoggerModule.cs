using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.Modules;
using Better.StateMachine.Runtime.States;
using UnityEngine;

namespace Odumbrata.Core.Modules
{
    public class LoggerModule<TState> : Module<TState> where TState : BaseState
    {
        protected override void OnStateChanged(IStateMachine<TState> stateMachine, TState state)
        {
            base.OnStateChanged(stateMachine, state);

            base.OnStateChanged(stateMachine, state);

            if (state == null)
            {
                return;
            }

            Debug.Log($"{state.GetType().Name} entered");
        }

        protected override void OnStatePreChanged(IStateMachine<TState> stateMachine, TState state)
        {
            base.OnStatePreChanged(stateMachine, state);

            var currentState = stateMachine.CurrentState;

            if (currentState == null)
            {
                return;
            }

            Debug.Log($"{currentState.GetType().Name} exited");
        }
    }
}