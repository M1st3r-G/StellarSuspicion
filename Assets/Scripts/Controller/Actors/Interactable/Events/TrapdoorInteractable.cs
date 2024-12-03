using Data;
using Manager;

namespace Controller.Actors.Interactable.Events
{
    public class TrapdoorInteractable : InteractableBase
    {
        private TrapdoorController _parent;

        protected override void Awake()
        {
            base.Awake();
            _parent = GetComponentInParent<TrapdoorController>();
        }
        
        protected override void TriggerInteraction()
        {
            AudioManager.PlayEffect(AudioEffect.TrapdoorStuck, transform.position);
            _parent.SetOpen(false);
            SetInteractionTo(false);
        }
    }
}