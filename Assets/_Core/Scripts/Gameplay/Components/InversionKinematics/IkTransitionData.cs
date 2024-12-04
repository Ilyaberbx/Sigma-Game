using Odumbrata.Components.InversionKinematics.Profiles;

namespace Odumbrata.Components.InversionKinematics
{
    public class IkTransitionData
    {
        public IIkProfile[] To { get; }
        public float Duration { get; }

        public IkTransitionData(float duration, params IIkProfile[] to)
        {
            To = to;
            Duration = duration;
        }
    }
}