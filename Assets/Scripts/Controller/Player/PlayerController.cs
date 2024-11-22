using Controller.Actors.Interactable;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] [Tooltip("The Action for Walking")]
        private InputActionReference walkingAction;
        
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

        private InteractableBase _currentlyOver;
        
        #endregion

        #region SetUp

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            lookingAction.action.performed += OnMouseInput;            
            // Movement in Update
        }

        #endregion

        #region InputHanling

        private void OnMouseInput(InputAction.CallbackContext ctx)
        {
            if (UIManager.PauseMenu.IsPaused) return;
            
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

        private void FixedUpdate()
        {
            //Movement
            Vector2 input = walkingAction.action.ReadValue<Vector2>() * walkSpeed;
            _rigidbody.velocity = input.x * transform.right + input.y * transform.forward;
            
            
            // RayTrace
            if (!playerCam.gameObject.activeSelf) return;

            Ray midpointRay = playerCam.ScreenPointToRay(new Vector3(playerCam.pixelWidth / 2f, playerCam.pixelHeight / 2f, 0));
            if (!Physics.Raycast(midpointRay, out RaycastHit hit, 50f, ~LayerMask.NameToLayer("Interaction")))
            {
                if(_currentlyOver != null) _currentlyOver.OnPointerExit(null);
                _currentlyOver = null;
                return;
            }
            
            Debug.LogError($"Raycast Hit: {hit.transform.name}");
            
            // Hit an Interactable
            InteractableBase interact = hit.transform.gameObject.GetComponent<InteractableBase>();

            // New one
            if (_currentlyOver != interact)
            {   
                if(_currentlyOver != null) _currentlyOver.OnPointerExit(null);
                
                _currentlyOver = interact;
                interact.OnPointerEnter(null);
            }
            
            if(Mouse.current.leftButton.wasPressedThisFrame) interact.OnPointerDown(null);
            if(Mouse.current.leftButton.wasReleasedThisFrame) interact.OnPointerUp(null);
        }

        #endregion
        
        #region ExternalChangeOfVariables

        public void ChangeSensitivity(float sensitivity)
        {
            lookSpeed = sensitivity;
            print(lookSpeed+" player controller looking speed");
        }
        #endregion
    }
}