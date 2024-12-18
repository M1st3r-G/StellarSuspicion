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

        [SerializeField] [Tooltip("The Dialogue Controller for the Creatures")]
        private DialogueUIController dialogueUIController;
        private CanvasGroup _settingsGroup;
        
        [SerializeField][Tooltip("A refrence to the Gameover UI Element")]
        private GameOverUIController gameOverUIController;
        
        // Publics
        public static UIManager Instance;
        public static InteractUIController InteractionUI => Instance.interactUIController;
        public static PauseMenuController PauseMenu => Instance.pauseMenu;
        public static SettingsController Settings => Instance.settingsMenu;
        public static DialogueUIController Dialogue => Instance.dialogueUIController;
        
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
            gameOverUIController.Hide();
        }

        #endregion
    }
}
