using System;
using System.Collections.Generic;
using Better.StateMachine.Runtime;
using Odumbrata.Core;
using Odumbrata.Core.Container;
using Odumbrata.Systems.Movement.Data;
using Odumbrata.Systems.Movement.States;
using UnityEngine;

namespace Odumbrata.Systems.Movement
{
    [Serializable]
    public class MovementSystem : BaseSystem
    {
        [SerializeField] private MovementConfig _config;

        public BaseMoveState CurrentMove => _moveStateMachine.CurrentState;

        private StateMachine<BaseMoveState> _moveStateMachine;

        private Dictionary<Type, BaseMoveState> _movesMap;

        public override void Initialize(ISystemsContainerReadonly container)
        {
            base.Initialize(container);

            _movesMap = new Dictionary<Type, BaseMoveState>();
            _moveStateMachine = new StateMachine<BaseMoveState>();
            _moveStateMachine.Run();

            foreach (var availableMove in _config.AvailableMoves)
            {
                availableMove.Initialize(Container);
                _movesMap.Add(availableMove.GetType(), availableMove);
            }
        }

        public void SetMove<TMoveState>() where TMoveState : BaseMoveState, new()
        {
            if (!TryGetState<TMoveState>(out var moveState)) return;

            _moveStateMachine.ChangeState(moveState);
        }

        public void SetMove<TMoveState, TData>(TData data) where TMoveState : BaseMoveState<TData>, new()
            where TData : BaseMoveData
        {
            if (!TryGetState<TMoveState>(out var rawState)) return;

            if (rawState is BaseMoveState<TData> processedState)
            {
                processedState.SetData(data);
                _moveStateMachine.ChangeState(rawState);
            }
        }

        private bool TryGetState<TMoveState>(out BaseMoveState moveState) where TMoveState : BaseMoveState, new()
        {
            var type = typeof(TMoveState);

            var success = _movesMap.TryGetValue(type, out moveState);

            return success;
        }
    }
}