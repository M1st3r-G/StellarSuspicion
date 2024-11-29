using UnityEditor.AssetImporters;
using UnityEngine;

namespace Controller.Actors.Interactable.Events
{
    public class FuseInteraction : InteractableBase
    {
        [SerializeField] ParticleSystem particles;
        [SerializeField] FuseBoxController parent;

        public void StartInteraction()
        {
            particles.Play();
            SetInteractionTo(true);
        }
        
        protected override void TriggerInteraction()
        {
            particles.Stop();
            SetInteractionTo(false);
            parent.Finished();
        }
    }
}