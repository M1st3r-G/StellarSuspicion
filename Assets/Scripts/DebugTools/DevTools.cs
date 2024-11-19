using Controller;
using Controller.Actors;
using UnityEngine;

namespace DebugTools
{
    public class DevTools : MonoBehaviour
    {
        public WindowController windowController;
        public CreatureController creatureController;
        public DoorController firstDoorController;
        public DoorController secondDoorController;
        public TrapdoorController trapdoorController;
        public bool showHidden;
    }
}