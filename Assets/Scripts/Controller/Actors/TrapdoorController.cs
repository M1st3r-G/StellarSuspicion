using System.Collections;
using UnityEngine;

namespace Controller.Actors
{
    public class TrapdoorController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The Transform of the trapdoor")] 
        private Transform trapdoor;
        
        [Header("Parameters")]
        [SerializeField] [Range(0f, 2f)] [Tooltip("How long it takes the trapdoor to open")]
        private float openTime;
        
        // Public
        public bool IsOpen { get; private set; }

        // State
        private Coroutine _closingRoutine;
        
        public void SetOpen(bool open)
        {
            if(_closingRoutine != null) StopCoroutine(_closingRoutine);
            _closingRoutine = StartCoroutine(ClosingRoutine(open));
        }
        
        private IEnumerator ClosingRoutine(bool open)
        {
            float elapsed = 0f;
        
            Quaternion startRotation = trapdoor.localRotation;
            Quaternion endRotation = open ? Quaternion.Euler(0f, 0f, -90f) : Quaternion.identity;
        
            IsOpen = open;

            while (elapsed <= openTime)
            {
                trapdoor.localRotation = Quaternion.Lerp(startRotation, endRotation, elapsed / openTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            trapdoor.localRotation = endRotation;
        }
    }
}