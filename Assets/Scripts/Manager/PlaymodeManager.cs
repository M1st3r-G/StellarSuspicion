using System;
using System.Collections;
using Controller.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace Manager
{
    public class PlaymodeManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] [Tooltip("The PlayerController for Sitting")]
        private PlayerSitController playerSit;
        
        [SerializeField] [Tooltip("The PlayerController for Standing")]
        private PlayerStandController playerStand;
        
        [SerializeField] [Tooltip("The Input action asset")]
        private InputActionAsset mainInputAsset;

        [Header("Parameters")]
        [SerializeField] [Range(0f, 3f)] [Tooltip("Time it Takes to swap the camera")]
        private float swappingTime;
        
        [SerializeField] [Tooltip("Weather to start standing or sitting")]
        private bool startStanding;
 
        
        // Temps/States
        public InputActionMap FirstPersonMap => mainInputAsset.actionMaps[2];
        public InputActionMap SittingMap => mainInputAsset.actionMaps[1];
        private PlayerBaseController CurrentPlayerController => _isSitting ? playerSit : playerStand;
        private Camera CurrentCamera => CurrentPlayerController.Camera;

        private bool _isSitting;
        
        public static PlaymodeManager Instance;

        #region Setup

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of CameraManager");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            mainInputAsset.actionMaps[0].Enable();
        }

        public void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void Start()
        {
            _isSitting = startStanding;
            SwitchPlayerControllers();
        }

        #endregion

        public static void StandUp() => Instance.SwitchStateTo(false);
        public static void SitDown() => Instance.SwitchStateTo(true);
        
        private void SwitchStateTo(bool sit)
        {
            if (_isSitting == sit) return;

            Transform targetCamTransform = sit ? playerSit.Camera.transform : playerStand.Camera.transform; 
            StartCoroutine(MoveCameraToTransform(CurrentCamera,targetCamTransform, SwitchPlayerControllers));
        }

        private void SwitchPlayerControllers()
        {
            if (_isSitting)
            {
                playerSit.Unpossess();
                playerStand.Possess();
            }
            else
            {
                playerStand.Unpossess();
                playerSit.Possess();
            }

            _isSitting = !_isSitting;
            Debug.Log("SwitchedState");
        }

        #region CameraChange
        
        private IEnumerator MoveCameraToTransform(Camera cam, Transform trfm, Action onFinished)
        {
            float elapsedTime = 0;
            
            cam.transform.GetPositionAndRotation(out Vector3 startingPosition, 
                                                 out Quaternion startingRotation);
            trfm.GetPositionAndRotation(out Vector3 targetPosition, 
                                        out Quaternion targetRotation);
            
            while (elapsedTime <= swappingTime)
            {
                cam.transform.SetPositionAndRotation(
                    Vector3.Lerp(startingPosition, targetPosition, elapsedTime / swappingTime),
                    Quaternion.Lerp(startingRotation, targetRotation, elapsedTime / swappingTime)
                );
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            onFinished?.Invoke();
        }

        #endregion
        
        public static void SetMouseTo(bool active)
        {
            if (active)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else Cursor.lockState = CursorLockMode.Locked;
        }

        public static void ReturnMouseToGame() => Instance.CurrentPlayerController.SetMouseState();
    }
}
