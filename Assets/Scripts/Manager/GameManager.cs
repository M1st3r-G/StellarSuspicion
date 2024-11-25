using Controller.Actors;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The Window With the Monsters")]
        private WindowController window;
        
        public static WindowController Window => Instance.window;
        
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
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    
        #endregion
    }
}
