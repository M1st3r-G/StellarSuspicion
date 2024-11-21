using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class CreatureCreator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("A list of all the ImageConvertMaterials")] 
        private List<Material> colors;
        
        private readonly Dictionary<CreatureComponentType, CreaturePartAsset[]> _allParts = new();

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
            
            _allParts[CreatureComponentType.Mouth] = Resources.LoadAll<CreaturePartAsset>("Parts/Mouth");
            _allParts[CreatureComponentType.Eye]   = Resources.LoadAll<CreaturePartAsset>("Parts/Eye");
            _allParts[CreatureComponentType.Nose]  = Resources.LoadAll<CreaturePartAsset>("Parts/Nose");
            _allParts[CreatureComponentType.Body]  = Resources.LoadAll<CreaturePartAsset>("Parts/Body");
            _allParts[CreatureComponentType.Head]  = Resources.LoadAll<CreaturePartAsset>("Parts/Head");
           
            Debug.LogWarning("Mouth: " + _allParts[CreatureComponentType.Mouth].Length);
            Debug.LogWarning("Eyes: " + _allParts[CreatureComponentType.Eye].Length);
            Debug.LogWarning("Noses: " + _allParts[CreatureComponentType.Nose].Length);
            Debug.LogWarning("Body: " + _allParts[CreatureComponentType.Body].Length);
            Debug.LogWarning("Head: " + _allParts[CreatureComponentType.Head].Length);
            
        }

        private void OnDestroy()
        {
            if(_instance == this) _instance = null;
        }

        #endregion

        private Part GetRandomPart(CreatureComponentType type)
            => _allParts[type][Random.Range(0, _allParts[type].Length)].part;

        private Material GetRandomColor() => colors[Random.Range(0, colors.Count)];
        
        public static CreatureData GetRandomCreature() => _instance.GetRandom();

        private CreatureData GetRandom() 
            => new("Random",
                GetRandomPart(CreatureComponentType.Mouth),
                GetRandomPart(CreatureComponentType.Eye),
                GetRandomPart(CreatureComponentType.Nose),
                GetRandomPart(CreatureComponentType.Body), 
                GetRandomPart(CreatureComponentType.Head), 
                GetRandomColor());
    }
}