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
        public BaseMove CurrentMove => _moveStateMachine.CurrentState;
        public bool IsStopped => _agent.isStopped;

        private StateMachine<BaseMove> _moveStateMachine;

        private Dictionary<Type, BaseMove> _movesMap;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value == false)
                {
                    Set<IdleMove, IdleData>(new IdleData(_agent));
                }

                _isActive = value;
            }
        }

        public override void Initialize(ISystemsContainerReadonly container)
        {
            base.Initialize(container);

            _movesMap = new Dictionary<Type, BaseMove>();
            _moveStateMachine = new StateMachine<BaseMove>();
            _moveStateMachine.Run();

            foreach (var availableMove in _config.AvailableMoves)
            {
                availableMove.Initialize(Container);
                _movesMap.Add(availableMove.GetType(), availableMove);
            }
        }

        public TMove Set<TMove>() where TMove : BaseMove, new()
        {
            if (!IsActive)
            {
                return null;
            }

            if (!TryGetMove<TMove>(out var move)) return null;

            _moveStateMachine.ChangeState(move);
            return (TMove)move;
        }

        public TMove Set<TMove, TData>(TData data) where TMove : BaseMove<TData>, new()
            where TData : BaseMoveData
        {
            if (!IsActive)
            {
                return null;
            }

            if (!TryGetMove<TMove>(out var rawMove)) return null;

            if (rawMove is BaseMove<TData> actualMove)
            {
                actualMove.SetData(data);
                _moveStateMachine.ChangeState(rawMove);

                return (TMove)rawMove;
            }

            return null;
        }

        private bool TryGetMove<TMoveState>(out BaseMove move) where TMoveState : BaseMove, new()
        {
            var type = typeof(TMoveState);

            var success = _movesMap.TryGetValue(type, out move);

            return success;
        }
    }
}