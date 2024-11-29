using Data;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Actors.Interactable.Buttons
{
    public class ButtonKillInteract: ButtonBaseInteract
    {
        [SerializeField] [Tooltip("The Other Button")]
        private ButtonEnterInteract buttonEnter;

        [FormerlySerializedAs("buttonSkip")] [SerializeField] [Tooltip("The Other Button")]
        private ButtonNextInteract buttonNext;

        
        protected override void OnButtonDown()
        {
            if (GameManager.Creature.CurrentCreature is null)
            {
                // ErrorSound
                return;
            }

            GameManager.ResolveCreature(AcceptMode.Rejected, GameManager.Creature.CurrentCreature.Value);
            
            buttonNext.SetInteractionTo(true);
            buttonEnter.SetInteractionTo(false);
        }
    }
}