using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Controller.Actors.Interactable.Table
{
    public class ScreenInteractController : InteractableBase
    {
        [SerializeField] private Vector3 cameraPosition;
        [SerializeField] private Quaternion cameraRotation;
        [SerializeField] private float swappingTime;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private TextMeshProUGUI textMesh;

        [SerializeField] private List<string> content;

        private int _currentIndex;
        
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
            FinishedRotation(targetPosition == Vector3.zero);
        }

        public void MoveLeft()
        {
            _currentIndex--;
            if (_currentIndex == -1) _currentIndex += content.Count;

            DisplayContent();
        }

        public void Exit()
        {
            StartCoroutine(MoveCameraToTransform(playerCamera, Vector3.zero, Quaternion.identity));
            SetInteractionTo(true);
        }
        
        public void MoveRight()
        {
            _currentIndex++;
            if (_currentIndex == content.Count) _currentIndex = 0;

            DisplayContent();
        }
        
        private void DisplayContent() => textMesh.text = content[_currentIndex];

        private void FinishedRotation(bool start)
        {
            SetInteractionTo(start);
            //TODO
        }
    }
}