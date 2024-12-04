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
            TutorialManager.SetFlag(TutorialManager.TutorialFlag.PressedButtonKill);
            
            if (trapdoor.IsBlocked)
            {   
                AudioManager.PlayEffect(AudioEffect.Error, transform.position);
                return;
            }
            
            AudioManager.PlayEffect(AudioEffect.ButtonClick,transform.position);
            SetInteractionTo(false);
            buttonEnter.SetInteractionTo(false);

            if (GameManager.Creature.CurrentCreature is not null)
                GameManager.ResolveCreature(CreatureAction.Die, GameManager.Creature);
            else Debug.LogError("There is an Issue with the Timing except if Tutoiral");
        }
    }
}