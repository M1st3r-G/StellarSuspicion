using System.Collections;
using UnityEngine;

namespace Controller.Actors
{
    public class WindowController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("This is the Transform of the Canvas with the Non See-through Blinder image")]
        [SerializeField] private Transform blinderTransform;
        [Tooltip("This is the CanvasGroup Monsters Canvas to hide them")]
        [SerializeField] private CanvasGroup monsterGroup;

        [Header("Parameters")]
        [Tooltip("The length to move up inorder to hide the Blinder image")]
        [Range(0f, 3f)] [SerializeField] private float up;
        [Tooltip("The Time it takes to change the Blinder State")]
        [Range(0f, 3f)][SerializeField] private float closingTime;
    
        // Public
        public bool IsOpen { get; private set; }

        // Temps
        private Coroutine _closingRoutine;

        private void Awake() => SetWindowOpened(true);

        #region WindowClosing

        public void SetWindowOpened(bool open)
        {
            if(_closingRoutine != null) StopCoroutine(_closingRoutine);
            _closingRoutine = StartCoroutine(ClosingRoutine(open));
        }
        
        private IEnumerator ClosingRoutine(bool open)
        {
            float elapsed = 0f;
        
            Vector3 startPosition = blinderTransform.localPosition;
            Vector3 endPosition = open ? up * Vector3.up : Vector3.zero;
        
            IsOpen = open;

            if (open) monsterGroup.alpha = 1f; 
            
            while (elapsed <= closingTime)
            {
                blinderTransform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsed / closingTime);
            
                elapsed += Time.deltaTime;
                yield return null;
            }

            blinderTransform.localPosition = endPosition;
            if (!open) monsterGroup.alpha = 0f;
        }

        #endregion
    }
}