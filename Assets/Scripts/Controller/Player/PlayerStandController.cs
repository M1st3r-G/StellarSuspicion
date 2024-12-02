using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PlayerStandController : PlayerBaseController
    {
        #region Variables

        [Header("References")]
        [SerializeField] [Tooltip("The Action for Walking")]
        private InputActionReference walkingAction;
        
        [SerializeField] [Tooltip("The Action for Looking")]
        private InputActionReference lookingAction;
        
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

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody>();
            lookingAction.action.performed += OnMouseInput;            
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
            Vector2 input = walkingAction.action.ReadValue<Vector2>() * walkSpeed;
            _rigidbody.velocity = input.x * transform.right + input.y * transform.forward;
        }

        #endregion

        protected override InputActionMap LoadActionMap() => PlaymodeManager.Instance.FirstPersonMap;
        public override void SetMouseState() => PlaymodeManager.SetMouseTo(false);

        public override void Possess()
        {
            base.Possess();
            gameObject.SetActive(true);
            MouseInputManager.Instance.SetCamera(playerCam);
            UIManager.InteractionUI.SetCenterTo(true);
        }
        
        public override void Unpossess()
        {
            base.Unpossess();
            gameObject.SetActive(false);
            MouseInputManager.Instance.SetInactive();
            UIManager.InteractionUI.SetCenterTo(false);
        }

        #region ExternalChangeOfVariables

        public void ChangeSensitivity(float sensitivity)
        {
            lookSpeed = sensitivity;
            print(lookSpeed+" player controller looking speed");
        }
        #endregion
    }
}