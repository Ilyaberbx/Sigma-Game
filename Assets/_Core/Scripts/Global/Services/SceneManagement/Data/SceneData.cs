using System;
using Eflatun.SceneReference;
using UnityEngine;

namespace Odumbrata.Global.Services.SceneManagement
{
    [Serializable]
    public sealed class SceneData
    {
        [SerializeField] private SceneReference _reference;
        [SerializeField] private SceneType _sceneType;

        public SceneType SceneType => _sceneType;
        public string Name => _reference.Name;
        public string Path => _reference.Path;
    }
}