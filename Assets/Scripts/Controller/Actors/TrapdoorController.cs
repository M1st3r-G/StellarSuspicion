using System.Collections;
using Controller.Actors.Interactable.Events;
using UnityEngine;

namespace Controller.Actors
{
    public class TrapdoorController : MonoBehaviour
    {
        private TrapdoorInteractable _interactable;
        
        [Header("Parameters")]
        [SerializeField] [Range(0f, 2f)] [Tooltip("How long it takes the trapdoor to open")]
        private float openTime;
        
        // Public
        public bool IsOpen { get; private set; }

        // State
        private Coroutine _closingRoutine;

        private void Awake()
        {
            _interactable = GetComponentInChildren<TrapdoorInteractable>();
        }

        public void SetOpen(bool open)
        {
            if(_closingRoutine != null) StopCoroutine(_closingRoutine);
            _closingRoutine = StartCoroutine(ClosingRoutine(open));
        }

        public void SetOpenAsEvent()
        {
            SetOpen(true);
            _interactable.SetInteractionTo(true);
        }

        private IEnumerator ClosingRoutine(bool open)
        {
            float elapsed = 0f;
        
            Quaternion startRotation = _interactable.transform.localRotation;
            Quaternion endRotation = open ? Quaternion.Euler(0f, 0f, -90f) : Quaternion.identity;
        
            IsOpen = open;

            while (elapsed <= openTime)
            {
                _interactable.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, elapsed / openTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            _interactable.transform.localRotation = endRotation;
        }
    }
}