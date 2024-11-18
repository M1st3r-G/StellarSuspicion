using System.Collections;
using UnityEngine;

namespace Manager
{
    public class CameraManager : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Reference to Static Camera at the Desk")]
        [SerializeField] private Camera sittingCamera;
        [Tooltip("Reference to Dynamic Camera of the First Person Controller")]
        [SerializeField] private Camera firstPersonCamera;
        
        [Header("Parameters")]
        [Tooltip("Time it Takes to swap the camera")]
        [Range(0f, 3f)]
        [SerializeField] private float swappingTime;
        
        // Temps/States
        private Vector3 _firstPersonCameraStartingPosition;
        private Vector3 _sittingCameraStartingPosition;
        private Quaternion _sittingCameraStartingRotation;
        private Quaternion _firstPersonCameraStartingRotation;
        
        public bool IsSitting => sittingCamera.gameObject.activeSelf;
        
        #region Setup

        private static CameraManager _instance;

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
        }
        
        #endregion

        #region CameraChange

        public static void SwitchCamera(bool isSitting) => _instance.InnerSwitchCamera(isSitting);

        private void InnerSwitchCamera(bool isSitting)
        {
            sittingCamera.gameObject.SetActive(!isSitting);
            firstPersonCamera.gameObject.SetActive(isSitting);
            StartCoroutine(CameraChange(isSitting));
            
            //Add Swap to sitting person player controller todo
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
    }
}
