using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.StateMachine.Runtime;

namespace Odumbrata.Behaviour
{
    public abstract class BaseStateBehaviour<TBehaviourState> : BaseBehaviourWithSystems
        where TBehaviourState : BaseBehaviourState
    {
        #region States

        private readonly StateMachine StateMachine = new StateMachine();

        protected override void Start()
        {
            base.Start();

            StateMachine.Run();
        }

        private Task SetStateAsync<TState>() where TState : TBehaviourState, new()
        {
            var state = new TState();

            SetupState(state);

            return StateMachine.ChangeStateAsync(state, destroyCancellationToken);
        }

        protected Task SetState<TState>(TState state) where TState : TBehaviourState
        {
            SetupState(state);

            return StateMachine.ChangeStateAsync(state, destroyCancellationToken);
        }

        protected void SetState<TState>() where TState : TBehaviourState, new()
        {
            SetStateAsync<TState>().Forget();
        }

        #endregion States

        protected virtual void SetupState(TBehaviourState state)
        {
            state.Initialize(SystemsContainer);
        }
    }
}