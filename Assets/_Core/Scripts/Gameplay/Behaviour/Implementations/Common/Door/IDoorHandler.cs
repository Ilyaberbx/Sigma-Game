using System.Threading.Tasks;
using UnityEngine;

namespace Odumbrata.Behaviour.Common.Door
{
    public interface IDoorHandler : IBehaviour
    {
        public Task HandleDoorPreOpening(DoorHandleData data);
    }

    public class DoorHandleData
    {
        public Vector3 InteractionPosition { get; }
        public Vector3 LookAtPosition { get; }
        public float TransitionDuration { get; }

        public DoorHandleData(Vector3 interactionPosition, Vector3 lookAtPosition, float transitionDuration)
        {
            InteractionPosition = interactionPosition;
            LookAtPosition = lookAtPosition;
            TransitionDuration = transitionDuration;
        }
    }
}