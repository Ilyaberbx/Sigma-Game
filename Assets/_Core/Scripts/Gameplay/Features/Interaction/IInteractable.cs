using UnityEngine;

namespace Odumbrata.Features.Interaction
{
    public interface IInteractable
    {
        public void StartInteraction(Transform interactor);
        public void FinishInteraction(Transform interactor);
    }
}