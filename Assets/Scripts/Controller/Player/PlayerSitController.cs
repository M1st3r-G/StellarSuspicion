using Controller.Actors.Interactable;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Player
{
    public class PlayerSitController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The InputAction for Standing Up")]
        private InputActionReference standUpAction;

        [Header("Parameters")] 
        [SerializeField] [Tooltip("The DeskInteraction to Disable when Sitting")]
        private DeskController deskInteraction;

        private void Awake()
        {
            standUpAction.action.performed += OnStandUpAction;
        }
        
        private void OnStandUpAction(InputAction.CallbackContext ctx)
        {
            deskInteraction.SetInteractionTo(true);
            PlaymodeManager.SwitchState(true);
        }
    }
}