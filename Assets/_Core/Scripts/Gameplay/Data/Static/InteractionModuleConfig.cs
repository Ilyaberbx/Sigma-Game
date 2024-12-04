using System;
using Odumbrata.Behaviour.Player;
using UnityEngine;

namespace Odumbrata.Data.Static
{
    [Serializable]
    public sealed class InteractionModuleConfig
    {
        [SerializeField] private Color _preInteractionOutlineColor;
        [SerializeField] private Color _interactionOutlineColor;
        [SerializeField] private float _interactionOutlineWidth;
        [SerializeField] private float _preInteractionOutlineWidth;
        [SerializeField] private PlayerBehaviour _player;
        public Color PreInteractionOutlineColor => _preInteractionOutlineColor;
        public Color InteractionOutlineColor => _interactionOutlineColor;
        public float InteractionOutlineWidth => _interactionOutlineWidth;
        public float PreInteractionOutlineWidth => _preInteractionOutlineWidth;
        public PlayerBehaviour Player => _player;
    }
}