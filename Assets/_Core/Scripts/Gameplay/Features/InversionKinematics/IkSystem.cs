using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Odumbrata.Core;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
using Odumbrata.Features.InversionKinematics.Profiles;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Odumbrata.Features.InversionKinematics
{
    [Serializable]
    public sealed class IkSystem : BaseSystem
    {
        [SerializeField] private RigBuilder _builder;
        private IIkProfile[] _currentProfiles;
        public IIkProfile[] CurrentProfiles => _currentProfiles;

        public override void Initialize(ISystemsContainerReadonly container)
        {
            base.Initialize(container);

            _currentProfiles = Array.Empty<IIkProfile>();
        }

        public override void Dispose()
        {
            base.Dispose();

            ClearImmediately();
        }

        public async Task ProcessTransition(IkTransitionData data)
        {
            var to = data.To;
            var halfDuration = data.Duration / 2;

            await Clear(halfDuration);

            _currentProfiles = to;

            await ApplyProfiles(to, halfDuration);
        }


        public void ClearImmediately()
        {
            _builder.Clear();
            _builder.layers.Clear();

            if (_currentProfiles == null) return;

            foreach (var profile in _currentProfiles)
            {
                profile.Dispose();
            }

            _currentProfiles = null;
        }

        private async Task ApplyProfiles(IIkProfile[] profiles, float duration)
        {
            foreach (var profile in profiles)
            {
                profile.Apply(_builder);
                _builder.Build();
            }

            await AllWeightsTo(1, duration);
        }

        public async Task Clear(float duration)
        {
            await AllWeightsTo(0, duration);
            ClearImmediately();
        }

        private async Task AllWeightsTo(float value, float duration, bool sequential = false)
        {
            var tasks = new List<Task>();

            foreach (var layer in _builder.layers)
            {
                var rig = layer.rig;

                if (rig == null)
                {
                    continue;
                }

                var tween = WeightTween(rig, value, duration);
                var task = tween.AsTask(DisposeCancellation);

                tasks.Add(task);
            }

            if (sequential)
            {
                foreach (var task in tasks)
                {
                    await task;
                }

                return;
            }

            await Task.WhenAll(tasks);
        }

        private TweenerCore<float, float, FloatOptions> WeightTween(Rig rig, float value, float duration)
        {
            return DOTween.To(() => rig.weight,
                x => rig.weight = x,
                value,
                duration);
        }
    }
}