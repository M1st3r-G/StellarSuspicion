namespace Controller.Actors.Interactable
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
            base.TriggerInteraction();
            _parent.SetOpen(false);
            SetInteractionTo(false);
        }
    }
}