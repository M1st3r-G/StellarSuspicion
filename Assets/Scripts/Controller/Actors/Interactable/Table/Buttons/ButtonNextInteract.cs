using Manager;
using UnityEngine;

namespace Controller.Actors.Interactable.Table.Buttons
{
    public class ButtonNextInteract : ButtonBaseInteract
    {
        [SerializeField] [Tooltip("The Other Button")]
        private ButtonEnterInteract buttonEnter;

        [SerializeField] [Tooltip("The Other Button")]
        private ButtonKillInteract buttonKill;
        
        protected override void OnButtonDown()
        {
            SetInteractionTo(false);
            GameManager.Window.SetWindowOpened(true);
            if (GameManager.Creature.ShowingCreature)
            {
                //Error Sound
                return;
            }

            Debug.Log("Let New Creature in");
            GameManager.Creature.SetToCreature(CreatureCreator.GetRandomCreature());

            buttonKill.SetInteractionTo(true);
            buttonEnter.SetInteractionTo(true);
        }
    }
}