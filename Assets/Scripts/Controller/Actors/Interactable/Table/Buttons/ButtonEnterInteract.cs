using Data;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Actors.Interactable.Table.Buttons
{
    public class ButtonEnterInteract : ButtonBaseInteract
    {
        [FormerlySerializedAs("buttonSkip")] [SerializeField] [Tooltip("The Other Button")]
        private ButtonNextInteract buttonNext;

        [SerializeField] [Tooltip("The Other Button")]
        private ButtonKillInteract buttonKill;
        
        protected override void OnButtonDown()
        {
            if (GameManager.Creature.CurrentCreature is null)
            {
                // ErrorSound 
                return;
            }

            GameManager.ResolveCreature(AcceptMode.Allowed, GameManager.Creature.CurrentCreature.Value);
            buttonKill.SetInteractionTo(false);
            buttonNext.SetInteractionTo(true);
        }
    }
}