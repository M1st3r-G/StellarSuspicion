using Controller;
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

        [SerializeField] private ClockController clockController;
        [SerializeField] private FuseBoxController fuseBoxController;
        
        public static MicrophoneInteractController Mic => Instance.microphoneInteraction;
        public static WindowController Window => Instance.window;
        public static CreatureController Creature => Instance.window.Creature;

        // Temps
        public int MonstersAmount { get; private set; }
        private int Rating { get; set; }
        private int GetRightAmount => (MonstersAmount + Rating) / 2;
        public float Accuracy => GetRightAmount / (float)(MonstersAmount - 4);
        private bool _success;


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
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #endregion

        public static void ResolveCreature(CreatureAction acceptAction, CreatureController creatureController) =>
            Instance.InnerResolveCreature(acceptAction, creatureController);

        private void InnerResolveCreature(CreatureAction acceptAction, CreatureController creatureController)
        {
             creatureController.GetGoodness(out CreatureAlignment creatureAlignment);
            if (creatureAlignment is CreatureAlignment.Neutral) creatureAlignment = CreatureAlignment.Evil;

            _success =  (acceptAction == CreatureAction.Die ? -1 : 1) * (int)creatureAlignment > 0;
     
            Debug.Log("Creature rating was " + (_success ? "correct" : "incorrect"));
            UIManager.Dialogue.ShowResolution(acceptAction, _success);
            Creature.Clear(acceptAction, _success);
        }

        public static void RateCreature(CreatureController creatureController) => Instance.InnerRateCreature(creatureController);

        private void InnerRateCreature(CreatureController creatureController)
        {
            creatureController.GetGoodness(out var alignment);
            if (alignment is CreatureAlignment.Neutral) alignment = CreatureAlignment.Evil;
            Debug.LogWarning(_success +" Success    "+ alignment + " Creature Aligment");
            if (!_success && alignment == CreatureAlignment.Evil)
            {
                _fatalErrors++;
                Debug.LogError("Start Printout");
                clockController.Printout();
            }

            if (_fatalErrors == 4) GameOverUIController.Instance.GameOver();
            Instance.MonstersAmount++;
            int rating = _success ? 1 : -1;
            Instance.Rating += rating;
            Debug.LogWarning("Total Rating: " + Instance.Rating + ", Total Fatal Errors: " + _fatalErrors);
        }
    }
}
