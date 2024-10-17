using System;
using Better.Attributes.Runtime.Select;
using Odumbrata.Features.Movement.States;
using UnityEngine;

namespace Odumbrata.Features.Movement
{
    [Serializable]
    public class MovementConfig
    {
        [SerializeReference, Select(typeof(BaseMoveState))] private BaseMoveState[] _availableMoves;

        public BaseMoveState[] AvailableMoves => _availableMoves;
    }
}