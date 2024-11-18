using System.Collections;
using UnityEngine;

namespace Controller
{
    public class WindowController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform blinderTransform;
        [SerializeField] private CanvasGroup monsterGroup;
    
        [Header("Parameters")]
        [SerializeField] private float up = 1.5f;
        [SerializeField] private float closingTime = 1f;
    
        // Publics
        public bool IsOpen { get; private set; }

        // Temps
        private Coroutine _closingRoutine;

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
        
            float startAlpha = monsterGroup.alpha;
            float endAlpha = open ? 0f: 1f;
        
            IsOpen = open;
        
            while (elapsed <= closingTime)
            {
                blinderTransform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsed / closingTime);
                monsterGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / closingTime);
            
                elapsed += Time.deltaTime;
                yield return null;
            }

            blinderTransform.localPosition = endPosition;
            monsterGroup.alpha = endAlpha;
        }

        #endregion
    }
}
