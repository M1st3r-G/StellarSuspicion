using Controller.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("A Reference to the Interaction UI element.")]
        private InteractUIController interactUIController;
        
        [SerializeField] [Tooltip("A Reference to the Interaction UI element.")]
        private CanvasGroup pauseMenu;
        
        [SerializeField][Tooltip("A Refrence to the Settings UI Element")]
        private CanvasGroup settingsMenu;
        
        [Header("Parameters")]
        [SerializeField] [Tooltip("Whether the Menus start Visible")]
        private bool pauseStartVisible;
        
        [SerializeField] [Tooltip("Whether the Settings Menus start Visible")]
        private bool settingsStartVisible;

        #region Setup

        // Publics
        public static UIManager Instance;
        public InteractUIController InteractionUI => interactUIController;
        
        public bool IsPaused => pauseMenu.alpha > 0.5f;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one UI Manager in scene.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            SetMenuActive(pauseMenu, pauseStartVisible);
            SetMenuActive(settingsMenu,settingsStartVisible );
        }
        

        #endregion
        private static void SetMenuActive(CanvasGroup menu, bool state)
        {
            menu.interactable = state;
            menu.blocksRaycasts = state;
            menu.alpha = state ? 1f : 0f;
        }

        public static void SetPauseActive(bool active) => SetMenuActive(Instance.pauseMenu, active);
        public static void SetSettingsMenuActive(bool active) => SetMenuActive(Instance.settingsMenu, active);
    }
}
