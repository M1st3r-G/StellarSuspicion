using Controller.Actors;
using Manager;
using UnityEngine;

namespace Controller
{
    public class ExitDoor : DoorController
    {
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

        private static void ShowError()
        {
            Debug.LogError("Creature Visible");
        }
    }
}