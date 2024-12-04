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
    public sealed class RoomsActivationModuleConfig
    {
        [SerializeField] private RoomsActivationConfig[] _activationsData;
        public RoomsActivationConfig[] ActivationsData => _activationsData;
    }

    [Serializable]
    public sealed class RoomsActivationConfig
    {
        [SerializeReference, Select(typeof(BaseRoomBehaviour))]
        private List<SerializedType> _activateOnOpen;

        [SerializeField] private BaseDoorBehaviour _door;

        public BaseDoorBehaviour Door => _door;
        public List<SerializedType> ActivateOnOpen => _activateOnOpen;
    }
}