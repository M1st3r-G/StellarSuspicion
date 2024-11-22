using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Player
{
    public abstract class PlayerBaseController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The Player Attached Camera")]
        protected Camera playerCam;
        protected InputActionMap ActionMap;

        public Camera Camera => playerCam;

        public Vector3 DefaultPos => DefaultPosition;
        protected Vector3 DefaultPosition;
        public Quaternion DefaultRot => DefaultRotation;
        protected Quaternion DefaultRotation;

        private void Awake() => playerCam.transform.GetPositionAndRotation(out DefaultPosition, out DefaultRotation);
        private void Start() => ActionMap = LoadActionMap();
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