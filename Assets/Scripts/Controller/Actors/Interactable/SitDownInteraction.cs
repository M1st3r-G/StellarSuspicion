using Manager;

namespace Controller.Actors.Interactable
{
    public class SitDownInteraction : InteractableBase
    {
        protected override void TriggerInteraction()
        {
            base.TriggerInteraction();

            PlaymodeManager.SitDown();
            SetInteractionTo(false);
        }
    }
}
