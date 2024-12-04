using Data;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Actors.Interactable.Buttons
{
    public class ButtonEnterInteract : ButtonBaseInteract
    {
        [FormerlySerializedAs("buttonSkip")] [SerializeField] [Tooltip("The Other Button")]
        private ButtonNextInteract buttonNext;

        [SerializeField] [Tooltip("The Other Button")]
        private ButtonKillInteract buttonKill;
        
        protected override void OnButtonDown()
        {
            AudioManager.PlayEffect(AudioEffect.ButtonClick,transform.position);
            TutorialManager.SetFlag(TutorialManager.TutorialFlag.PressedButtonExit);
            buttonKill.SetInteractionTo(false);
            SetInteractionTo(false);
            
            if (GameManager.Creature.CurrentCreature is not null) 
                GameManager.ResolveCreature(CreatureAction.Exit, GameManager.Creature);
            else Debug.LogError("There is an Issue with the Timing except if Tutoiral");
        }
    }
}