using Controller.Actors;
using Controller.Actors.Interactable.Table;
using Controller.Creature;
using Controller.UI.Panels;
using Data;
using Extern;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The Window With the Monsters")]
        private WindowController window;

        [SerializeField] [Tooltip("The Microphone")]
        private MicrophoneInteractController microphoneInteraction;

        public static MicrophoneInteractController Mic => Instance.microphoneInteraction;
        public static WindowController Window => Instance.window;
        public static CreatureController Creature => Instance.window.Creature;
        
        // Temps
        private int MonstersAmount { get; set; }
        private int Rating { get; set; }
        private int GetRightAmount => (MonstersAmount + Rating) / 2;
        private int GetWrongAmount => (MonstersAmount - Rating) / 2;
        public float Accuracy => GetRightAmount / (float)(MonstersAmount - 5);
        private static bool _success;
        private static int _score;
        
        

        private static int _fatalErrors;
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
            
            TimeManager.OnDayEnd += () => Debug.Log("Game Over");
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        private void Start()
        {
            TimeManager.Instance.StartTimerActive();
            Debug.Log("Tag Beginnt");
        }

        #endregion

        public static void ResolveCreature(CreatureAction acceptAction, CreatureController creatureController)
        {
            CreatureAlignment creatureAlignment = creatureController.IsGood();
            if (creatureAlignment is CreatureAlignment.Neutral) creatureAlignment = CreatureAlignment.Evil;

            _success =  (acceptAction == CreatureAction.Die ? -1 : 1) * (int)creatureAlignment > 0;
     
            Debug.Log("Creature rating was " + (_success ? "correct" : "incorrect"));
            UIManager.Dialogue.ShowResolution(acceptAction, _success);
            Creature.Clear(acceptAction, _success);
        }

        public static void RateCreature(CreatureController creatureController)
        {
            if (!_success && creatureController.IsGood()== CreatureAlignment.Evil)_fatalErrors++;
            if (_fatalErrors == 5)GameOverUIController.instance.GameOver(_score-5); ;
            Instance.MonstersAmount++;
            int rating = _success ? 1 : -1;
            Instance.Rating += rating;
            Debug.LogWarning("Total Rating: " + Instance.Rating +", Total Fatal Errors: " + _fatalErrors);
        }
    }
}
