using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float closingtime = 1f;
    [SerializeField] Camera sittingCamera;
    [SerializeField] Camera firstPersonCamera;
    Vector3 firstPersonCameraStartingPosition;
    Vector3 sittingCameraStartingPosition;
    Quaternion sittingCameraStartingRotation;
    Quaternion firstPersonCameraStartingRotation;
    #region Setup

    public static CameraManager Instance;

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
    }

    public void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    #endregion

    public void Start()
    {
        sittingCameraStartingPosition=sittingCamera.transform.position;
        sittingCameraStartingRotation=sittingCamera.transform.rotation;
        firstPersonCameraStartingPosition=firstPersonCamera.transform.position;
        firstPersonCameraStartingRotation=firstPersonCamera.transform.rotation;
        firstPersonCamera.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SwitchCamera(true);
        }

        if (Input.GetKeyDown("f"))
        {
            SwitchCamera(false);
        }
    }

    public void SwitchCamera(bool isSitting)
    {
        if (isSitting)
        {
            sittingCamera.gameObject.SetActive(false);
            firstPersonCamera.gameObject.SetActive(true);
            StartCoroutine(CameraChange(isSitting));
            //Add Swap to first person player controller todo
        }
        else
        {
            sittingCamera.gameObject.SetActive(true);
            firstPersonCamera.gameObject.SetActive(false);
            StartCoroutine(CameraChange(isSitting));

            //Add Swap to sitting person player controller todo

        }
    }

    private IEnumerator CameraChange(bool isSitting)
    {
        float elapsedTime = 0;
        
        Vector3 startingPosition;
        Vector3 targetPosition;
        Quaternion startingRotation;
        Quaternion targetRotation;
        Camera cam;
        
        if (isSitting)
        {
            startingPosition = sittingCameraStartingPosition;
            targetPosition = firstPersonCameraStartingPosition;
            
            startingRotation = sittingCameraStartingRotation;
            targetRotation = firstPersonCameraStartingRotation;
            
            cam = firstPersonCamera;
        }
        else
        {
            startingPosition = firstPersonCameraStartingPosition;
            targetPosition = sittingCameraStartingPosition;
            
            startingRotation = firstPersonCameraStartingRotation;
            targetRotation = sittingCameraStartingRotation;
            
            cam = sittingCamera;
        }
        
        
        while (elapsedTime <= closingtime)
        {
            cam.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / closingtime);
            cam.transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, elapsedTime / closingtime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Camera Change");
        
    }
}
