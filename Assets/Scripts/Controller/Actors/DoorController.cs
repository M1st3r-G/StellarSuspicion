using System.Collections;
using Data;
using Manager;
using UnityEngine;

namespace Controller.Actors
{
    public class DoorController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("This is the Transform of the Canvas with the Non See-through Blinder image")]
        [SerializeField] private Transform doorTransform;
        
        [Header("Parameters")]
        [Tooltip("The length to move up inorder to hide the Blinder imae")]
        [Range(0f, 3f)] [SerializeField] private float up;
        [Tooltip("The Time it takes to change the Blinder State")]
        [Range(0f, 3f)][SerializeField] private float closingTime;
    
        // Public
        public bool IsOpen { get; private set; }
        
        // Temps
        private Coroutine _closingRoutine;
        
        //DoorCollider logic
        private void OnTriggerEnter(Collider other) => SetDoorOpened(true);
        private void OnTriggerExit(Collider other) => SetDoorOpened(false);

        #region DoorClosing

        //Bool open must be true for the door to open
        protected virtual void SetDoorOpened(bool open)
        {
            AudioManager.PlayEffect(AudioEffect.DoorCreak,transform.position);
            
            if(_closingRoutine != null) StopCoroutine(_closingRoutine);
            _closingRoutine = StartCoroutine(ClosingRoutine(open));
        }
        
        private IEnumerator ClosingRoutine(bool open)
        {
            float elapsed = 0f;
        
            Vector3 startPosition = doorTransform.localPosition;
            Vector3 endPosition = open ? up * Vector3.up : Vector3.zero;
        
            IsOpen = open;
        
            
            while (elapsed <= closingTime)
            {
                doorTransform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsed / closingTime);
            
                elapsed += Time.deltaTime;
                yield return null;
            }
            doorTransform.localPosition = endPosition;
        }

        #endregion
    }
}
