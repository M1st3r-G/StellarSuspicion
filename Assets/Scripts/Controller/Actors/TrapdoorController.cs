using System.Collections;
using Controller.Actors.Interactable.Events;
using Data;
using Manager;
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
        public bool IsBlocked { get; private set; }

        // State
        private Coroutine _closingRoutine;

        private void Awake() => _interactable = GetComponentInChildren<TrapdoorInteractable>();

        public void SetOpen(bool open)
        {
            if(_closingRoutine != null) StopCoroutine(_closingRoutine);
            _closingRoutine = StartCoroutine(MoveToBlockRoutine(open));
        }

        public void SetOpenAsEvent()
        {
            SetOpen(true);
            AudioManager.PlayEffect(AudioEffect.TrapdoorStuck, transform.position);
            _interactable.SetInteractionTo(true);
        }

        private IEnumerator MoveToBlockRoutine(bool blocked)
        {
            float elapsed = 0f;
        
            Quaternion startRotation = _interactable.transform.localRotation;
            Quaternion endRotation = blocked ? Quaternion.Euler(0f, -3f, -6f) : Quaternion.identity;
        
            IsBlocked = blocked;

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