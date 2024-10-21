using System;
using System.Collections.Generic;
using Better.StateMachine.Runtime;
using Odumbrata.Core;
using Odumbrata.Core.Container;
using Odumbrata.Features.Movement.Data;
using Odumbrata.Features.Movement.States;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Features.Movement
{
    [Serializable]
    public sealed class MovementSystem : BaseSystem
    {
        [SerializeField] private bool _isActive;
        [SerializeField] private MovementConfig _config;
        [SerializeField] private NavMeshAgent _agent;
        public BaseMoveState CurrentMove => _moveStateMachine.CurrentState;
        public bool IsStopped => _agent.isStopped;

        private StateMachine<BaseMoveState> _moveStateMachine;

        private Dictionary<Type, BaseMoveState> _movesMap;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value == false)
                {
                    Set<IdleState, IdleData>(new IdleData(_agent));
                }

                _isActive = value;
            }
        }

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

        public void Set<TMoveState>() where TMoveState : BaseMoveState, new()
        {
            if (!IsActive)
            {
                return;
            }

            if (!TryGetMove<TMoveState>(out var moveState)) return;

            _moveStateMachine.ChangeState(moveState);
        }

        public void Set<TMoveState, TData>(TData data) where TMoveState : BaseMoveState<TData>, new()
            where TData : BaseMoveData
        {
            if (!IsActive)
            {
                return;
            }

            if (!TryGetMove<TMoveState>(out var rawState)) return;

            if (rawState is BaseMoveState<TData> processedState)
            {
                processedState.SetData(data);
                _moveStateMachine.ChangeState(rawState);
            }
        }

        private bool TryGetMove<TMoveState>(out BaseMoveState moveState) where TMoveState : BaseMoveState, new()
        {
            var type = typeof(TMoveState);

            var success = _movesMap.TryGetValue(type, out moveState);

            return success;
        }
    }
}