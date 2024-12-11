using System;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Levels.Modules;
using Odumbrata.Behaviour.Rooms.Abstractions;
using Odumbrata.Core.EventSystem;
using Odumbrata.Core.Modules;
using Odumbrata.Data.Static;
using UnityEngine;

namespace Odumbrata.Behaviour.Rooms.Debug
{
    public sealed class DebugRoomModule : BaseRoomModule, IConfigurableModule<DebugRoomModuleConfig>
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private readonly ConfigurableModule<DebugRoomModuleConfig> _configurable = new();

        public DebugRoomModuleConfig Config => _configurable.Config;

        public override async Task Initialize(Type context, EventSystem events)
        {
            await base.Initialize(context, events);
            Events.Subscribe<RoomActivatedArg>(OnRoomActivated);
            Events.Subscribe<RoomDeactivatedArg>(OnRoomDeactivated);
        }

        public override void Dispose()
        {
            base.Dispose();

            Events.Unsubscribe<RoomActivatedArg>(OnRoomActivated);
            Events.Unsubscribe<RoomDeactivatedArg>(OnRoomDeactivated);
        }

        public void SetConfiguration(DebugRoomModuleConfig config)
        {
            _configurable.SetConfiguration(config);
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