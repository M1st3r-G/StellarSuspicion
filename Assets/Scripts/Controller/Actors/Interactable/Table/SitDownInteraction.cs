using Manager;

namespace Controller.Actors.Interactable.Table
{
    public class SitDownInteraction : InteractableBase
    {
        protected override void TriggerInteraction()
        {
            PlaymodeManager.SitDown();
            SetInteractionTo(false);
        }
    }
}
