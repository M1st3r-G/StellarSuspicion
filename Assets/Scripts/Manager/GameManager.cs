using System;
using Controller;
using Controller.Actors;
using Controller.Creature;
using Data;
using Extern;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The Window With the Monsters")]
        private WindowController window;
        
        public static WindowController Window => Instance.window;
        public static CreatureController Creature => Instance.window.Creature;
        
        // Temps
        public int MonstersAmount { get; private set; }
        public int Rating { get; private set; }
        private int GetRightAmount => (MonstersAmount + Rating) / 2;
        private int GetWrongAmount => (MonstersAmount - Rating) / 2;
        public float Accuracy => GetRightAmount / (float)(MonstersAmount - 5);
        private static bool success;
        
        private static int score;
        

        private static int fatalErrors;
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

        public static void ResolveCreature(CreatureAction acceptAction, CreatureData creature)
        {
            CreatureAlignment creatureAlignment = creature.IsGood();
            if (creatureAlignment is CreatureAlignment.Neutral)
                creatureAlignment = Random.Range(0f, 1f) > 0.5f ? CreatureAlignment.Good : CreatureAlignment.Evil;

            success =  (acceptAction == CreatureAction.Die ? -1 : 1) * (int)creatureAlignment > 0;
     
            Debug.Log("Creature rating was " + (success ? "correct" : "incorrect"));
            UIManager.Dialogue.ShowResolution(acceptAction, success);
            Creature.Clear(acceptAction, success);
        }

        public static void RateCreature(CreatureData creature)
        {
            if (!success && creature.IsGood()== CreatureAlignment.Evil)fatalErrors++;
            if (fatalErrors == 5)GameOverUIController.instance.GameOver(score-5); ;
            Instance.MonstersAmount++;
            int rating = success ? 1 : -1;
            Instance.Rating += rating;
            score++;
            Debug.LogWarning("Total Rating: " + Instance.Rating +", Total Fatal Errors: " + fatalErrors);
        }
    }
}
