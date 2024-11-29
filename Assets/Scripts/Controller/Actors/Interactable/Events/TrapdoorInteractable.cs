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
            _parent.SetOpen(false);
            SetInteractionTo(false);
        }
    }
}