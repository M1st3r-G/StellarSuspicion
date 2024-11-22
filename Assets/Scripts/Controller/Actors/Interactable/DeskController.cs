using Manager;

namespace Controller.Actors.Interactable
{
    public class DeskController : InteractableBase
    {
        protected override void TriggerInteraction()
        {
            base.TriggerInteraction();

            PlaymodeManager.SwitchState(false);
            SetInteractionTo(false);
        }
    }
}
