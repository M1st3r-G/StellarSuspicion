using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.UI.Panels
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PauseMenuController : UIPanel
    {
        [Header("References")]
        [Tooltip("The InputAction to pause the Game")]
        [SerializeField] private InputActionReference pauseAction;

        public bool IsPaused { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            pauseAction.action.performed += OnPauseAction;
        }

        private void OnPauseAction(InputAction.CallbackContext context)
        {
            if (!IsPaused) Pause();
            else if (IsVisible) Unpause();
            else
            {
                // Paused and Invisible => Settings
                UIManager.Settings.SetMenuActive(false);
                SetMenuActive(true);
            }
        }

        private void Pause()
        {
            Debug.Log("Game is paused!");
            Time.timeScale = 0;

            SetMenuActive(true);
            IsPaused = true;
            PlaymodeManager.SetMouseTo(true);
        }

        public void Unpause()
        {
            Debug.Log("You have unpaused the GameManager!");
            Time.timeScale = 1;
            
            SetMenuActive(false);
            IsPaused = false;
            PlaymodeManager.ReturnMouseToGame();
        }

        public void OpenSettings()
        {
            SetMenuActive(false);
            UIManager.Settings.SetMenuActive(true);
        }

        public void QuitToMainMenu()
        {
            Debug.LogError("you quit to main menu");
            Application.Quit();
        }
    }
}