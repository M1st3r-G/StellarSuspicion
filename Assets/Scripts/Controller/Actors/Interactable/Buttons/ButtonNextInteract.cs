using Data;
using Manager;
using UnityEngine;

namespace Controller.Actors.Interactable.Buttons
{
    public class ButtonNextInteract : ButtonBaseInteract
    {
        [SerializeField] [Tooltip("The Other Button")]
        private ButtonEnterInteract buttonEnter;

        [SerializeField] [Tooltip("The Other Button")]
        private ButtonKillInteract buttonKill;
        
        protected override void OnButtonDown()
        {
            GameManager.Window.SetWindowOpened(true);
            AudioManager.PlayEffect(AudioEffect.ButtonClick,transform.position);

            GameManager.Creature.SetToCreature(CreatureCreator.GetRandomCreature());

            SetInteractionTo(false);
            buttonKill.SetInteractionTo(true);
            buttonEnter.SetInteractionTo(true);
        }
    }
}