using System;
using System.Collections.Generic;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Odumbrata.Behaviour.Rooms.Abstractions;
using UnityEngine;

namespace Odumbrata.Data.Static
{
    [Serializable]
    public sealed class RoomsInitializationModule
    {
        [SerializeField] private BaseRoomBehaviour[] _rooms;
        [SerializeReference, Select(typeof(BaseRoomBehaviour))] private List<SerializedType> _activeOnStart;
        public BaseRoomBehaviour[] Rooms => _rooms;
        public List<SerializedType> ActiveOnStart => _activeOnStart;
    }
}