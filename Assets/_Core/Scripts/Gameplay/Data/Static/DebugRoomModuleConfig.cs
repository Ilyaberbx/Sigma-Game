using System;
using System.Collections.Generic;
using UnityEngine;

namespace Odumbrata.Data.Static
{
    [Serializable]
    public sealed class DebugRoomModuleConfig
    {
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private List<MeshRenderer> _renderers;

        public Color ActiveColor => _activeColor;
        public Color InactiveColor => _inactiveColor;
        public List<MeshRenderer> Renderers => _renderers;
    }
}