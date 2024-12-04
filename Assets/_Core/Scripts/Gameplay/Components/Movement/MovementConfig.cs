using System;
using Better.Attributes.Runtime.Select;
using Odumbrata.Components.Movement.States;
using UnityEngine;

namespace Odumbrata.Components.Movement
{
    [Serializable]
    public class MovementConfig
    {
        [SerializeReference, Select] private BaseMove[] _availableMoves;

        public BaseMove[] AvailableMoves => _availableMoves;
    }
}