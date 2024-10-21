using System.Threading.Tasks;
using Odumbrata.Core;
using Odumbrata.Features.InversionKinematics.Profiles;

namespace Odumbrata.Features.InversionKinematics
{
    public sealed class IkSystem : BaseSystem
    {
        public Task SetProfile<TProfile>(IkTransitionData data) where TProfile : BaseIkProfile
        {
            return Task.CompletedTask;
        }
    }

    public class IkTransitionData
    {
        public IkTransitionData(float duration)
        {
            Duration = duration;
        }

        public float Duration { get; }
    }
}