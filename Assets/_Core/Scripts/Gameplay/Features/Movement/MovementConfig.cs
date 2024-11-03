using System;
using Better.Attributes.Runtime.Select;
using Odumbrata.Features.Movement.States;
using UnityEngine;

namespace Odumbrata.Features.Movement
{
    [Serializable]
    public class MovementConfig
    {
        [SerializeReference, Select] private BaseMove[] _availableMoves;

        public BaseMove[] AvailableMoves => _availableMoves;
    }
}