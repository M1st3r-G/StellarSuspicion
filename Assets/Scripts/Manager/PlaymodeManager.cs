using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace Manager
{
    public class PlaymodeManager : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Reference to Static Camera at the Desk")] [SerializeField] 
        private Camera sittingCamera;
        
        [SerializeField] [Tooltip("Reference to Dynamic Camera of the First Person Controller")]
        private Camera firstPersonCamera;
        
        [SerializeField] [Tooltip("The Input action asset")]
        private InputActionAsset mainInputAsset;
        
        [Header("Parameters")]
        [SerializeField] [Range(0f, 3f)] [Tooltip("Time it Takes to swap the camera")]
         private float swappingTime;
        
        private InputActionMap FirstPersonMap => mainInputAsset.actionMaps[2];
        private InputActionMap SittingMap => mainInputAsset.actionMaps[1];
        
        // Temps/States
        private Vector3 _firstPersonCameraStartingPosition;
        private Vector3 _sittingCameraStartingPosition;
        private Quaternion _sittingCameraStartingRotation;
        private Quaternion _firstPersonCameraStartingRotation;
        
        public bool IsSitting => sittingCamera.gameObject.activeSelf;
        
        #region Setup

        public static PlaymodeManager _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("More than one instance of CameraManager");
                Destroy(gameObject);
                return;
            }
            Debug.Log("Camera Manager");
            _instance = this;
        }

        public void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }

        public void Start()
        {
            _sittingCameraStartingPosition = sittingCamera.transform.position;
            _sittingCameraStartingRotation = sittingCamera.transform.rotation;
            
            _firstPersonCameraStartingPosition = firstPersonCamera.transform.position;
            _firstPersonCameraStartingRotation = firstPersonCamera.transform.rotation;
            
            firstPersonCamera.gameObject.SetActive(false);
            
            mainInputAsset.actionMaps[0].Enable();
            SittingMap.Enable();
        }
        
        #endregion

        public static void SwitchState(bool isSitting)
        {
            _instance.SwitchInputMap(isSitting);
            _instance.SwitchCamera(isSitting);
        }

        #region CameraChange
        
        private void SwitchCamera(bool isSitting)
        {
            sittingCamera.gameObject.SetActive(!isSitting);
            firstPersonCamera.gameObject.SetActive(isSitting);
            StartCoroutine(CameraChange(isSitting));
        }

        private IEnumerator CameraChange(bool isSitting)
        {
            float elapsedTime = 0;
        
            Vector3 startingPosition    = isSitting ? _sittingCameraStartingPosition : _firstPersonCameraStartingPosition;
            Quaternion startingRotation = isSitting ? _sittingCameraStartingRotation : _firstPersonCameraStartingRotation;
            
            Vector3 targetPosition    = isSitting ? _firstPersonCameraStartingPosition : _sittingCameraStartingPosition;
            Quaternion targetRotation = isSitting ? _firstPersonCameraStartingRotation : _sittingCameraStartingRotation;
            
            Camera currentCamera = isSitting ? firstPersonCamera : sittingCamera;
        
        
            while (elapsedTime <= swappingTime)
            {
                currentCamera.transform.SetPositionAndRotation(
                    Vector3.Lerp(startingPosition, targetPosition, elapsedTime / swappingTime),
                    Quaternion.Lerp(startingRotation, targetRotation, elapsedTime / swappingTime)
                );
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        #endregion
        
        #region InputChange
        
        private void SwitchInputMap(bool isSitting)
        {
            if (isSitting)
            {
                FirstPersonMap.Enable();
                SittingMap.Disable();
            }
            else
            {
                FirstPersonMap.Disable();
                SittingMap.Enable();
            }
            
            ReturnMouseToGame();
        }

        public static void SetMouseTo(bool active)
        {
            if (active)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public static void ReturnMouseToGame() => SetMouseTo(_instance.IsSitting);

        #endregion
    }
}
