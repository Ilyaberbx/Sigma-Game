using System;
using Better.Services.Runtime;
using UnityEngine;

namespace Odumbrata.Core.Installers
{
    [Serializable]
    public sealed class MonoInstaller : BaseInstaller<MonoService>
    {
        [SerializeField] private MonoService[] _services;
        protected override MonoService[] Services => _services;
    }
}