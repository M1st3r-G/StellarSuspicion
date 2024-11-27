using System.Collections;
using UnityEngine;

namespace Controller.Actors.Interactable
{
    public class ScreenInteractController : InteractableBase
    {
        [SerializeField] private Vector3 cameraPosition;
        [SerializeField] private Quaternion cameraRotation;
        [SerializeField] private float swappingTime;
        [SerializeField] private Camera playerCamera;
        
        protected override void TriggerInteraction()
        {
            // Move Camera to Screen
            StartCoroutine(MoveCameraToTransform(playerCamera, cameraPosition, cameraRotation));
        }
        
        private IEnumerator MoveCameraToTransform(Camera cam, Vector3 targetPosition, Quaternion targetRotation)
        {
            float elapsedTime = 0;
            
            cam.transform.GetLocalPositionAndRotation(out Vector3 startingPosition, 
                out Quaternion startingRotation);
            
            while (elapsedTime <= swappingTime)
            {
                cam.transform.SetLocalPositionAndRotation(
                    Vector3.Lerp(startingPosition, targetPosition, elapsedTime / swappingTime),
                    Quaternion.Lerp(startingRotation, targetRotation, elapsedTime / swappingTime)
                );
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            cam.transform.SetLocalPositionAndRotation(targetPosition, targetRotation);
        }
    }
}