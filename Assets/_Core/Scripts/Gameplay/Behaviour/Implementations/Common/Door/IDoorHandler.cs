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
        public Vector3 InteractionPosition { get; }
        public Vector3 LookAtPosition { get; }

        public DoorTransitionData(Vector3 interactionPosition, Vector3 lookAtPosition)
        {
            InteractionPosition = interactionPosition;
            LookAtPosition = lookAtPosition;
        }
    }
}