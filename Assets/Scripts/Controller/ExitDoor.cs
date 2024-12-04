using Controller.Actors;
using Data;
using Manager;
using UnityEngine;

namespace Controller
{
    public class ExitDoor : DoorController
    {
        public Animator errorSpriteAnim;
        
        protected override void SetDoorOpened(bool open)
        {
            if(!open) base.SetDoorOpened(false);
            else
            {
                //Showing
                if (!GameManager.Creature.HasCreature) base.SetDoorOpened(true);
                else ShowError();
            }
        }

        private void ShowError()
        {
            errorSpriteAnim.SetTrigger("Try");
            AudioManager.PlayEffect(AudioEffect.Error, transform.position);
        }
    }
}