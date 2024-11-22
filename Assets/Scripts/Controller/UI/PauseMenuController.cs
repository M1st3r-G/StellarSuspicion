using Extern;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PauseMenuController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("The InputAction to pause the Game")]
        [SerializeField] private InputActionReference pauseAction;

        public bool IsPaused { get; private set; }

        private CanvasGroup _myGroup;
        
        private void Awake()
        {
            _myGroup = GetComponent<CanvasGroup>();
            pauseAction.action.performed += OnPauseAction;
        }

        private void OnPauseAction(InputAction.CallbackContext context)
        {
            switch (IsPaused)
            {
                case false:
                    Pause();
                    return;
                // Paused and Visible
                case true when _myGroup.alpha > 0.5:
                    Unpause();
                    return;
                // Paused and Invisible => Settings
                case true when _myGroup.alpha < 0.5:
                    UIManager.Settings.SetMenuActive(false);
                    _myGroup.SetGroupActive(true);
                    break;
            }
        }

        private void Pause()
        {
            Debug.LogWarning("Game is paused!");
            Time.timeScale = 0;
            
            _myGroup.SetGroupActive(true);
            IsPaused = true;
            PlaymodeManager.SetMouseTo(true);
        }

        public void Unpause()
        {
            Debug.LogWarning("You have unpaused the GameManager!");
            Time.timeScale = 1;
            
            _myGroup.SetGroupActive(false);
            IsPaused = false;
            PlaymodeManager.ReturnMouseToGame();
        }

        public void OpenSettings()
        {
            _myGroup.SetGroupActive(false);
            UIManager.Settings.SetMenuActive(true);
        }

        public void QuitToMainMenu()
        {
            Debug.LogError("you quit to main menu");
            Application.Quit();
        }

        public void SetMenuActive(bool b) => _myGroup.SetGroupActive(b);
    }
}