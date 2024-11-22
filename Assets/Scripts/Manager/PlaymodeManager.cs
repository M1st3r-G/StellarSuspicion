using System.Collections;
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
        private Vector3 _fpcStartingPosition;
        private Vector3 _scStartingPosition;
        private Quaternion _scStartingRotation;
        private Quaternion _fpcStartingRotation;

        private bool IsSitting => sittingCamera.gameObject.activeSelf;
        
        #region Setup

        public static PlaymodeManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of CameraManager");
                Destroy(gameObject);
                return;
            }
            Debug.Log("Camera Manager");
            Instance = this;

            mainInputAsset.actionMaps[0].Enable();
        }

        public void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void Start()
        {
            sittingCamera.transform.GetPositionAndRotation(out _scStartingPosition, out _scStartingRotation);
            firstPersonCamera.transform.GetPositionAndRotation(out _fpcStartingPosition, out _fpcStartingRotation);
            
            SwitchState(false);
        }
        
        #endregion

        public static void SwitchState(bool isSitting)
        {
            Debug.Log("SwitchingState");
            Instance.SwitchCamera(isSitting);
            Instance.SwitchInputMap(isSitting);
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
        
            Vector3 startingPosition    = isSitting ? _scStartingPosition : _fpcStartingPosition;
            Quaternion startingRotation = isSitting ? _scStartingRotation : _fpcStartingRotation;
            
            Vector3 targetPosition    = isSitting ? _fpcStartingPosition : _scStartingPosition;
            Quaternion targetRotation = isSitting ? _fpcStartingRotation : _scStartingRotation;
            
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
            else Cursor.lockState = CursorLockMode.Locked;
        }

        public static void ReturnMouseToGame() => SetMouseTo(Instance.IsSitting);

        #endregion
    }
}
