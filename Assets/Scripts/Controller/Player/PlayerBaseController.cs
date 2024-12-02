using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Player
{
    [DefaultExecutionOrder(-1)]
    public abstract class PlayerBaseController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The Player Attached Camera")]
        protected Camera playerCam;
        protected InputActionMap ActionMap;

        public Camera Camera => playerCam;

        public Vector3 DefaultPos => _defaultPosition;
        private Vector3 _defaultPosition;
        public Quaternion DefaultRot => _defaultRotation;
        private Quaternion _defaultRotation;

        protected virtual void Awake() => playerCam.transform.GetPositionAndRotation(out _defaultPosition, out _defaultRotation);
        protected virtual void Start() => ActionMap = LoadActionMap();
        protected abstract InputActionMap LoadActionMap();

        public virtual void Possess()
        {
            Camera.gameObject.SetActive(true);
            ActionMap.Enable();
            SetMouseState();
        }

        public abstract void SetMouseState();

        public virtual void Unpossess()
        {
            Camera.gameObject.SetActive(false);
            ActionMap.Disable();
            playerCam.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
        
        
    }
}