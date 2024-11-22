using System;
using Controller.Actors.Interactable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manager
{
    public class MouseInputManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputActionReference mouseInteract;
        
        //States
        private InteractableBase _currentlyOver;
        private bool IsActive => _currentCamera is not null;
        private Camera _currentCamera;
        private int _defaultMask;
        private Vector3 midPoint;
        
        public static MouseInputManager Instance { get; private set; }

        #region SetUp

        private void Awake()
        {
            if (Instance is not null)
            {
                Debug.LogWarning("There Were multiple MouseInputManagers");
                Destroy(this);
                return;
            }

            Instance = this;
            
            _defaultMask = ~LayerMask.NameToLayer("Interaction");
            
            mouseInteract.action.started += MouseDown;
            mouseInteract.action.canceled += MouseUp;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #endregion

        private void MouseDown(InputAction.CallbackContext obj)
        {
            if(!IsActive) return;
            _currentlyOver?.OnPointerDown(null);
        }
        
        private void MouseUp(InputAction.CallbackContext obj)
        {
            if(!IsActive) return;
            _currentlyOver?.OnPointerUp(null);
        }

        public void SetInactive() => _currentCamera = null;

        public void SetCamera(Camera newCamera)
        {
            _currentCamera = newCamera;
            midPoint = new Vector3(_currentCamera.pixelWidth / 2f, _currentCamera.pixelHeight / 2f, 0);
        }
        
        private void FixedUpdate()
        {
            if (!IsActive) return;
            
            if (!Physics.Raycast(_currentCamera.ScreenPointToRay(midPoint), out RaycastHit hit, 5f, _defaultMask))
            {
                Debug.LogError("No Raycast Hit");
                if (_currentlyOver == null) return;
                
                _currentlyOver.OnPointerExit(null);
                _currentlyOver = null;
                return;
            }

            Debug.LogError("Raycast Hit");
            
            // Old One
            if (_currentlyOver is not null && _currentlyOver.transform == hit.transform) return;
            
            // New one
            if (_currentlyOver != null) _currentlyOver.OnPointerExit(null);
            _currentlyOver = hit.transform.gameObject.GetComponent<InteractableBase>();
            _currentlyOver.OnPointerEnter(null);
        }
    }
}