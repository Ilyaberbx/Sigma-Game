using System;
using Odumbrata.Behaviour.Rooms.Abstractions;
using UnityEngine;

namespace Odumbrata.Data.Static
{
    [Serializable]
    public sealed class RoomsCoreModuleConfig
    {
        [SerializeField] private BaseRoomBehaviour[] _rooms;
        public BaseRoomBehaviour[] Rooms => _rooms;
    }
}