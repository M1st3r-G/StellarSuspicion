using System.Collections;
using Controller;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        // Temps
        public int Score { get; private set; }
        public int NumMonsters { get; private set; }
        public int NumSuccesses { get; private set; }

        [SerializeField] private CreatureController creature;
        
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

        private void Start()
        {
            StartCoroutine(ShowOffMonsters());
        }

        private IEnumerator ShowOffMonsters()
        {
            while (true)
            {
                creature.SetToCreature(CreatureCreator.GetRandomCreature());
                yield return new WaitForSeconds(1f);
            }
        }

        #endregion
    }
}
