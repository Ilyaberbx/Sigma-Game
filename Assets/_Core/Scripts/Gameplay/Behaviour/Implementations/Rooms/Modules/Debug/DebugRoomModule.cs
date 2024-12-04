using System;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Behaviour.Rooms.Abstractions;
using Odumbrata.Core.EventSystem;
using Odumbrata.Data.Static;
using UnityEngine;

namespace Odumbrata.Behaviour.Rooms.Debug
{
    public sealed class DebugRoomModule : BaseRoomModule<DebugRoomModuleConfig>
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        public override void Initialize(Type context, EventSystem events)
        {
            base.Initialize(context, events);

            Events.Subscribe<RoomActivatedArg>(OnRoomActivated);
            Events.Subscribe<RoomDeactivatedArg>(OnRoomDeactivated);
        }

        public override void Dispose()
        {
            base.Dispose();

            Events.Unsubscribe<RoomActivatedArg>(OnRoomActivated);
            Events.Unsubscribe<RoomDeactivatedArg>(OnRoomDeactivated);
        }

        private void OnRoomActivated(object sender, RoomActivatedArg arg)
        {
            if (!IsContext(arg.ContextType))
            {
                return;
            }

            Draw(Config.ActiveColor);
        }

        private void OnRoomDeactivated(object sender, RoomDeactivatedArg arg)
        {
            if (!IsContext(arg.ContextType))
            {
                return;
            }

            Draw(Config.InactiveColor);
        }

        private void Draw(Color color)
        {
            if (Config.Renderers.IsNullOrEmpty())
            {
                return;
            }

            foreach (var renderer in Config.Renderers)
            {
                var materialProperty = new MaterialPropertyBlock();
                materialProperty.SetColor(BaseColor, color);
                renderer.SetPropertyBlock(materialProperty);
            }
        }
    }
}