using System;
using Better.Attributes.Runtime.Select;
using Better.Services.Runtime;
using UnityEngine;

namespace Odumbrata.Core.Installers
{
    [Serializable]
    public sealed class PocoInstaller : BaseInstaller<PocoService>
    {
        [SerializeReference, Select] private PocoService[] _services;
        protected override PocoService[] Services => _services;
    }
}