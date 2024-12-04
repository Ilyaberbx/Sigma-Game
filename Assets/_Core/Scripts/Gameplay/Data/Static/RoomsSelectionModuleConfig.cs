using System;
using System.Collections.Generic;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Odumbrata.Behaviour.Common.Door;
using Odumbrata.Behaviour.Rooms.Abstractions;
using UnityEngine;

namespace Odumbrata.Data.Static
{
    [Serializable]
    public sealed class RoomsSelectionModuleConfig
    {
        [SerializeField] private RoomSelectionConfig[] _transitionsData;

        public RoomSelectionConfig[] TransitionsData => _transitionsData;
    }

    [Serializable]
    public sealed class RoomSelectionConfig
    {
        [SerializeReference, Select(typeof(BaseRoomBehaviour))]
        private List<SerializedType> _activateOnOpen;

        [SerializeField] private BaseDoorBehaviour _door;

        public BaseDoorBehaviour Door => _door;

        public List<SerializedType> ActivateOnOpen => _activateOnOpen;
    }
}