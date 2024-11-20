using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("The InputAction to pause the Game")]
        [SerializeField] private InputActionReference pauseAction;
        
        // Temps
        public int Score { get; private set; }
        public int NumMonsters { get; private set; }
        public int NumSuccesses { get; private set; }

        // Public
        public static GameManager Instance;
        
        #region Setup

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one GameManager in scene!");
                Destroy(gameObject);
                return; 
            }
            
            Debug.Log("GameManager is created!");
            Instance = this;

            pauseAction.action.performed += ctx => Pause();
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    
        #endregion
        
        #region Pause

        private static void Pause()
        {
            Debug.LogWarning("GameManager is paused!");
            Time.timeScale = 0;
            PlaymodeManager.SetMouseOnOf(true);
        }

        public static void Unpause()
        {
            Debug.LogWarning("GameManager is unpaused!");
            Time.timeScale = 1;
            PlaymodeManager.SetMouseOnOf(false);
        }
        #endregion
    }
}
