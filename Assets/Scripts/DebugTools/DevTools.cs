using Controller;
using UnityEngine;

namespace DebugTools
{
    public class DevTools : MonoBehaviour
    {
        public WindowController windowController;
        public CreatureController creatureController;
        public DoorController firstDoorController;
        public DoorController secondDoorController;
        public bool showHidden;
    }
}