using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Manager
{
    public class CreatureCreator : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("A list of all of the monsters possible Eyes")]
        [SerializeField] private List<CreaturePartAsset> monsterEyes;
        [Tooltip("A list of all of the monsters possible Mouths")]
        [SerializeField] private List<CreaturePartAsset> monsterMouths;

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

            Debug.Assert(monsterEyes.All(p => p.part.type == CreatureComponentType.Eye), "monsterEyes contains bad stuff");
            Debug.Assert(monsterMouths.All(p => p.part.type == CreatureComponentType.Mouth), "monsterMouth contains bad stuff");
        }

        private void OnDestroy()
        {
            if(_instance == this) _instance = null;
        }

        #endregion

        public static CreatureData GetRandomCreature() => _instance.GetRandom();

        private CreatureData GetRandom() 
            => new("Random", 
            monsterMouths[Random.Range(0, monsterMouths.Count)].part,
            monsterEyes[Random.Range(0, monsterEyes.Count)].part);
    }
}