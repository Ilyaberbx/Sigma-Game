using System;
using System.Linq;
using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Odumbrata.Global.Services.SceneManagement
{
    [Serializable]
    public class ScenesGroup
    {
        [SerializeField] private string _name;
        [SerializeField] private SceneData[] _scenes;

        public SceneData[] Scenes => _scenes;

        public string Name => _name;

        public string FindSceneNameByType(SceneType type)
        {
            return Scenes.FirstOrDefault(temp => temp.SceneType == type)?.Name;
        }

        public int GetScenesCount()
        {
            return Scenes.IsNullOrEmpty() ? 0 : Scenes.Length;
        }
    }
}