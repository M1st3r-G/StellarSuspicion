using Controller.UI;
using UnityEngine;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("A Reference to the Interaction UI element.")]
        private InteractUIController interactUIController;
        
        [SerializeField] [Tooltip("A Reference to the Interaction UI element.")]
        private CanvasGroup pauseMenu;
        
        // Publics
        public static UIManager Instance;
        public InteractUIController InteractionUI => interactUIController;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one UI Manager in scene.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            SetMenuActive(pauseMenu, true);
        }

        private static void SetMenuActive(CanvasGroup menu, bool state)
        {
            menu.interactable = state;
            menu.blocksRaycasts = state;
            menu.alpha = state ? 1f : 0f;
        }
    }
}
