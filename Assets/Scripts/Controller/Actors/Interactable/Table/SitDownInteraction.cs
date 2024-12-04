using Manager;

namespace Controller.Actors.Interactable.Table
{
    public class SitDownInteraction : InteractableBase
    {
        protected override void TriggerInteraction()
        {
            TutorialManager.SetFlag(TutorialManager.TutorialFlag.SatDown);
            PlaymodeManager.SitDown();
            SetInteractionTo(false);
        }
    }
}
