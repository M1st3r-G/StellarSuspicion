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

        [SerializeField] [Tooltip("The Trapdoor Controller")]
        private TrapdoorController trapdoor;
        
        protected override void OnButtonDown()
        {
            Debug.Assert(GameManager.Creature.CurrentCreature is not null, "There is an Issue with the Timing");

            if (trapdoor.IsBlocked)
            {
                AudioManager.PlayEffect(AudioEffect.Error, transform.position);
                return;
            }
            
            GameManager.ResolveCreature(CreatureAction.Die, GameManager.Creature.CurrentCreature.Value);
            
            buttonNext.SetInteractionTo(true);
            SetInteractionTo(false);
            buttonEnter.SetInteractionTo(false);
        }
    }
}