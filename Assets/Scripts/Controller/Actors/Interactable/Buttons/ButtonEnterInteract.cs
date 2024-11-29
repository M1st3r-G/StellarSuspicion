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
            Debug.Assert(GameManager.Creature.CurrentCreature is not null, "There is an Issue with the Timing");

            GameManager.ResolveCreature(AcceptMode.Allowed, GameManager.Creature.CurrentCreature.Value);
            buttonKill.SetInteractionTo(false);
            SetInteractionTo(false);
            buttonNext.SetInteractionTo(true);
        }
    }
}