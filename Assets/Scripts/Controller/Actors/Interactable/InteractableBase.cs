using Manager;
using QuickOutline;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller.Actors.Interactable
{
    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(Outline), typeof(Collider))]
    public class InteractableBase : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Parameters")]
        [SerializeField] [Range(0.5f, 4f)] [Tooltip("The Amount of time, the Object has to be clicked")]
        private float threshold;

        [SerializeField] [Tooltip("Whether to start Enabled")]
        private bool startEnabled = true;

        private float _holdTime;
        private float HoldTime
        {
            get => _holdTime;
            set
            {
                _holdTime = value;
                UIManager.InteractionUI.SetToAmount(_holdTime / threshold);
            }
        }

        private bool _held;
        private bool IsEnabled=> _isEnabled && !UIManager.PauseMenu.IsPaused;
        private bool _isEnabled;
        private Outline _outline;

        #region UpdateAndAwake

        protected virtual void Awake()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
            
            _isEnabled = startEnabled;
            gameObject.layer = LayerMask.NameToLayer("Interaction");
        }

        private void Start()
        {
            // CleanUp Errors of others
            Renderer tmpR = GetComponent<Renderer>();
            tmpR.materials = new[] { tmpR.materials[0] };
        }

        private void Update()
        {
            if (!IsEnabled) return;
            if (!_held) return;

            HoldTime += Time.deltaTime;

            if (HoldTime < threshold) return;
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
            HoldTime = 0f;
        }

        public void SetInteractionTo(bool pEnabled)
        {
            _isEnabled = pEnabled;
            _outline.enabled = false;
        }

        #endregion

        #region MouseInput

        public void OnPointerEnter(PointerEventData eventData) => _outline.enabled = IsEnabled;
        public void OnPointerExit(PointerEventData eventData) => _outline.enabled = false;
        public void OnPointerDown(PointerEventData eventData) => _held = IsEnabled;
        public void OnPointerUp(PointerEventData eventData) => Release();

        #endregion
    }
}