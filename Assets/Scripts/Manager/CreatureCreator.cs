using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Manager
{
    public class CreatureCreator : MonoBehaviour
    {
        private readonly Dictionary<CreatureComponentType, CreaturePartAsset[]> _allParts = new();

        // Temps/States
        private static CreatureCreator _instance;

        private Part GetRandomPart(CreatureComponentType type)
            => _allParts[type][Random.Range(0, _allParts[type].Length)].part;
        
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
            
            _allParts[CreatureComponentType.Mouth] = Resources.LoadAll<CreaturePartAsset>("Parts/Mouth");
            _allParts[CreatureComponentType.Eye]   = Resources.LoadAll<CreaturePartAsset>("Parts/Eye");
            _allParts[CreatureComponentType.Nose]  = Resources.LoadAll<CreaturePartAsset>("Parts/Nose");
           
            Debug.LogWarning("Mouth: " + _allParts[CreatureComponentType.Mouth].Length);
            Debug.LogWarning("Eyes: " + _allParts[CreatureComponentType.Eye].Length);
            Debug.LogWarning("Noses: " + _allParts[CreatureComponentType.Nose].Length);
            
        }

        private void OnDestroy()
        {
            if(_instance == this) _instance = null;
        }

        #endregion

        public static CreatureData GetRandomCreature() => _instance.GetRandom();

        private CreatureData GetRandom() 
            => new("Random",
                GetRandomPart(CreatureComponentType.Mouth),
                GetRandomPart(CreatureComponentType.Eye),
                GetRandomPart(CreatureComponentType.Nose));
    }
}