using Controller.UI;
using Controller.UI.Panels;
using UnityEngine;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("A Reference to the Interaction UI element.")]
        private InteractUIController interactUIController;
        
        [SerializeField] [Tooltip("A Reference to the Interaction UI element.")]
        private PauseMenuController pauseMenu;
        private CanvasGroup _pauseGroup;
        
        [SerializeField][Tooltip("A Reference to the Settings UI Element")]
        private SettingsController settingsMenu;
        private CanvasGroup _settingsGroup;
        
        [Header("Parameters")]
        [SerializeField] [Tooltip("Whether the Menus start Visible")]
        private bool pauseStartVisible;
        
        [SerializeField] [Tooltip("Whether the Settings Menus start Visible")]
        private bool settingsStartVisible;

        // Publics
        public static UIManager Instance;
        public static InteractUIController InteractionUI => Instance.interactUIController;
        public static PauseMenuController PauseMenu => Instance.pauseMenu;
        public static SettingsController Settings => Instance.settingsMenu;
        
        #region Setup
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one UI Manager in scene.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            Settings.SetMenuActive(false);
            PauseMenu.SetMenuActive(false);
        }

        #endregion
    }
}
