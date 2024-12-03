using Controller.UI;
using Manager;

namespace Controller.Actors.Interactable.Table
{
    public class MicrophoneInteractor : InteractableBase
    {
        protected override void TriggerInteraction()
        {
            DialogueUIController.ShowQuestionOptions();
        }
    }
}