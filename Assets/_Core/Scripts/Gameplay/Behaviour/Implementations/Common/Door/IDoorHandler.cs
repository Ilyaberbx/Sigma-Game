using System.Threading.Tasks;
using UnityEngine;

namespace Odumbrata.Behaviour.Common.Door
{
    public interface IDoorHandler
    {
        public Task HandleDoorOpeningStarted(DoorInteractionData data);
        public Task HandleDoorOpeningEnded(DoorInteractionData data);

        public Transform Transform { get; }
    }

    public class DoorInteractionData
    {
        public Vector3 InteractionPosition { get; }

        public DoorInteractionData(Vector3 interactionPosition)
        {
            InteractionPosition = interactionPosition;
        }
    }
}