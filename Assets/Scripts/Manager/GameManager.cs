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

            Instance.MonstersAmount++;
            int rating = (acceptAction == CreatureAction.Die ? -1 : 1) * (int)creatureAlignment;
            Instance.Rating += rating;
            
            Debug.LogWarning("Creature rating was " + (rating > 0.5f ? "correct" : "incorrect"));
            UIManager.Dialogue.ShowInteraction(acceptAction, default, rating > 0);
            Creature.Clear(acceptAction, rating > 0);
        }
    }
}
