using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] [Tooltip("The Action for Walking")]
        private InputActionReference walkingAction;
        
        [SerializeField] [Tooltip("The Action for Interacting")]
        private InputActionReference interactAction;
        
        [SerializeField] [Tooltip("The Action for Looking")]
        private InputActionReference lookingAction;

        [SerializeField] [Tooltip("The Player Attached Camera")]
        private Camera playerCam;
        
        private Rigidbody _rigidbody;
        
        
        [Header("Parameters")]
        [SerializeField] [Range(1f, 5f)] [Tooltip("The Speed of the Player in Units per Second")]
        private float walkSpeed;
        
        [SerializeField] [Range(0.1f, 0.5f)] [Tooltip("The Speed the camera Rotates")]
        private float lookSpeed;

        [SerializeField] [Range(0.1f, 0.9f)] [Tooltip("Half the visible Range for looking up and down")]
        private float halfRange;
        

        #endregion

        #region SetUp

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            lookingAction.action.performed += OnMouseInput;            
            interactAction.action.performed += OnInteract;
            walkingAction.action.performed += OnMovementInput;
            walkingAction.action.canceled += OnStopMovement;
        }

        #endregion

        #region InputHanling

        private void OnInteract(InputAction.CallbackContext ctx)
        {
            Debug.Log("Interact");
        }

        private void OnMouseInput(InputAction.CallbackContext ctx)
        {
            Vector2 input = ctx.ReadValue<Vector2>() * lookSpeed;
            
            // Rotate around y
            transform.localRotation *= Quaternion.AngleAxis(input.x, Vector3.up);
            
            // Rotate camera around x
            float height = playerCam.transform.localRotation.x;

            // Block extremes ander rollover
            if (height < -halfRange && input.y > 0) return;
            if (height > halfRange && input.y < 0) return;

            playerCam.transform.localRotation *= Quaternion.AngleAxis(-input.y, Vector3.right);
        }
        
        private void OnMovementInput(InputAction.CallbackContext ctx)
        {
            Vector2 input = ctx.ReadValue<Vector2>() * walkSpeed;
            _rigidbody.velocity = input.x * transform.right + input.y * transform.forward;
        }

        private void OnStopMovement(InputAction.CallbackContext ctx) => _rigidbody.velocity = Vector3.zero;

        #endregion
    }
}