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

        public static MicrophoneInteractController Mic => _instance.microphoneInteraction;
        public static WindowController Window => _instance.window;
        public static CreatureController Creature => _instance.window.Creature;
        
        // Temps
        private int MonstersAmount { get; set; }
        private int Rating { get; set; }
        private int GetRightAmount => (MonstersAmount + Rating) / 2;
        private float Accuracy => GetRightAmount / (float)(MonstersAmount - 5);
        private bool _success;
        
        

        private static int _fatalErrors;
        // Public
        private static GameManager _instance;
        
        #region Setup

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("More than one GameManager in scene!");
                Destroy(gameObject);
                return; 
            }
            
            Debug.Log("GameManager is created!");
            _instance = this;
        }

        private void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }

        private void Start()
        {
            TimeManager.Instance.StartTimerActive();
            Debug.Log("Tag Beginnt");
        }

        #endregion

        public static void ResolveCreature(CreatureAction acceptAction, CreatureController creatureController) =>
            _instance.InnerResolveCreature(acceptAction, creatureController);

        private void InnerResolveCreature(CreatureAction acceptAction, CreatureController creatureController)
        {
            CreatureAlignment creatureAlignment = creatureController.IsGood();
            if (creatureAlignment is CreatureAlignment.Neutral) creatureAlignment = CreatureAlignment.Evil;

            _success =  (acceptAction == CreatureAction.Die ? -1 : 1) * (int)creatureAlignment > 0;
     
            Debug.Log("Creature rating was " + (_success ? "correct" : "incorrect"));
            UIManager.Dialogue.ShowResolution(acceptAction, _success);
            Creature.Clear(acceptAction, _success);
        }

        public static void RateCreature(CreatureController creatureController) => _instance.InnerRateCreature(creatureController);

        private void InnerRateCreature(CreatureController creatureController)
        {
            if (!_success && creatureController.IsGood()== CreatureAlignment.Evil)_fatalErrors++;
            if (_fatalErrors == 5) GameOverUIController.Instance.GameOver(Accuracy);
            _instance.MonstersAmount++;
            int rating = _success ? 1 : -1;
            _instance.Rating += rating;
            Debug.LogWarning("Total Rating: " + _instance.Rating +", Total Fatal Errors: " + _fatalErrors);
        }
    }
}
