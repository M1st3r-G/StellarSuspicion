using Manager;

namespace Controller.Actors.Interactable.Table
{
    public class MicrophoneInteractController : InteractableBase
    {
        protected override void TriggerInteraction()
        {
            UIManager.Dialogue.ShowQuestionOptions();
            SetInteractionTo(false);
        }
    }
}