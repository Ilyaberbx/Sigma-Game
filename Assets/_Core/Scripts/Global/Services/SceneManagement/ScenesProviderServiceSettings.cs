using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Odumbrata.Global.States;
using UnityEngine;

namespace Odumbrata.Global.Services.SceneManagement
{
    [CreateAssetMenu(menuName = "Configs/Services/Scenes", fileName = "New scenes provider settings", order = 0)]
    public class ScenesProviderServiceSettings : ScriptableObject
    {
        [SerializeField] private SceneGroupPair[] _groupsMap;

        public SceneGroupPair[] GroupsMap => _groupsMap;
    }

    [Serializable]
    public class SceneGroupPair
    {
        [SerializeReference, Select(typeof(BaseGameState))]
        private SerializedType _stateType;

        [SerializeField] private ScenesGroup _group;

        public SerializedType StateType => _stateType;
        public ScenesGroup Group => _group;
    }
}