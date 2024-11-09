using System.Threading.Tasks;
using UnityEngine;

namespace Odumbrata.Behaviour.Common.Door
{
    public interface IDoorHandler : IBehaviour
    {
        public Task HandleDoorPreOpening(DoorTransitionData data);
        public Task HandleDoorPostOpening(DoorTransitionData data);
    }

    public class DoorTransitionData
    {
        public float Duration { get; }
        public Vector3 InteractionPosition { get; }
        public Vector3 LookAtPosition { get; }

        public DoorTransitionData(Vector3 interactionPosition, Vector3 lookAtPosition, float duration)
        {
            InteractionPosition = interactionPosition;
            LookAtPosition = lookAtPosition;
            Duration = duration;
        }
    }
}