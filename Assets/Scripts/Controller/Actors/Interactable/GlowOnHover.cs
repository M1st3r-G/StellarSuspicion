using QuickOutline;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller.Actors.Interactable
{
    [RequireComponent(typeof(Outline))]
    public class GlowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Outline _outline;

        private void Awake()
        {
            _outline = GetComponent<Outline>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.LogWarning("Entered");
            _outline.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.LogWarning("Exit");
            _outline.enabled = false;
        }
    }
}