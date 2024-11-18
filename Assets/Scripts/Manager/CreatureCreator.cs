using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Manager
{
    public class CreatureCreator : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("A list of all of the monsters possible Eyes")]
        [SerializeField] private List<Sprite> monsterEyes;
        [Tooltip("A list of all of the monsters possible Mouths")]
        [SerializeField] private List<Sprite> monsterMouths;

        // Temps/States
        private static CreatureCreator _instance;

        #region SetUp

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("More than one Creature Creator attached");
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
        }

        private void OnDestroy()
        {
            if(_instance == this) _instance = null;
        }

        #endregion

        public static CreatureData GetRandomCreature()
        {
            return new CreatureData("Random",
                _instance.monsterMouths[Random.Range(0, _instance.monsterMouths.Count)],
                _instance.monsterEyes[Random.Range(0, _instance.monsterEyes.Count)]);
        }
    }
}