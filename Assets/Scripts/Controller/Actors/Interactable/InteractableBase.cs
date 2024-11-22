using QuickOutline;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller.Actors.Interactable
{
    [RequireComponent(typeof(Outline))]
    public class InteractableBase : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Parameters")]
        [SerializeField] [Range(0.5f, 4f)] [Tooltip("The Amount of time, the Object has to be clicked")]
        private float threshold;

        [SerializeField] [Tooltip("Whether to start Enabled")]
        private bool startEnabled = true;
        
        private float _holdTime;
        private bool _held;
        private bool _isEnabled;
        private Outline _outline;

        #region UpdateAndAwake

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
            
            _isEnabled = startEnabled;
        }

        private void Update()
        {
            if (!_isEnabled) return;
            if (!_held) return;

            _holdTime += Time.deltaTime;
            
            if (_holdTime < threshold) return;
            TriggerInteraction();
            Release();
        }

        #endregion

        #region Overridable

        protected virtual void TriggerInteraction()
        {
            Debug.Log("I was Triggered");
        }

        #endregion

        #region Utils

        private void Release()
        {
            _held = false;
            _holdTime = 0f;
        }

        public void SetInteractionTo(bool pEnabled) => _isEnabled = pEnabled;

        #endregion

        #region MouseInput

        public void OnPointerEnter(PointerEventData eventData) => _outline.enabled = _isEnabled;
        public void OnPointerExit(PointerEventData eventData) => _outline.enabled = false;
        public void OnPointerDown(PointerEventData eventData) => _held = _isEnabled;
        public void OnPointerUp(PointerEventData eventData) => Release();

        #endregion
    }
}