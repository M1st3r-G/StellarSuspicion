using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class CreatureCreator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("A list of all the ImageConvertMaterials")] 
        private List<Material> colors;
        
        [SerializeField] [Tooltip("A list of all the Possible Names")]
        private List<string> names;
        
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
            _allParts[CreatureComponentType.Gear]  = Resources.LoadAll<CreaturePartAsset>("Parts/Gear");
            
            Debug.Log(_allParts.Aggregate("",
                (msg, valuePair) => msg + $"{valuePair.Value[0].part.type}: {valuePair.Value.Length}\n"));
        }

        private void OnDestroy()
        {
            if(_instance == this) _instance = null;
        }

        #endregion

        public static void PrintDistribution()
        {
            foreach (var (key, value) in _instance.GetDistribution()) Debug.LogWarning($"The Value {key} appears {value}% of the time");
        }

        private Dictionary<int, float> GetDistribution()
        {
            Dictionary<int, float> ratingToAmount = new();

            int totalNumber = _allParts.Aggregate(1, (i, pair) => i * pair.Value.Length);
            
            foreach (var headAmount in _allParts[CreatureComponentType.Head].GroupBy(partList => partList.part.goodness).Select(g => new {Rating = g.Key, Count = g.Count()}))
            {
                foreach (var eyeAmount in _allParts[CreatureComponentType.Eye].GroupBy(partList => partList.part.goodness).Select(g => new { Rating = g.Key, Count = g.Count() }))
                {
                    foreach (var noseAmount in _allParts[CreatureComponentType.Nose].GroupBy(partList => partList.part.goodness).Select(g => new { Rating = g.Key, Count = g.Count() }))
                    {
                        foreach (var bodyAmount in _allParts[CreatureComponentType.Body].GroupBy(partList => partList.part.goodness).Select(g => new { Rating = g.Key, Count = g.Count() }))
                        {
                            foreach (var mouthAmount in _allParts[CreatureComponentType.Mouth].GroupBy(partList => partList.part.goodness).Select(g => new { Rating = g.Key, Count = g.Count() }))
                            {
                                foreach (var gearAmount in _allParts[CreatureComponentType.Gear].GroupBy(partList => partList.part.goodness).Select(g => new {Rating = g.Key, Count = g.Count() }))
                                {
                                    int rating = headAmount.Rating + eyeAmount.Rating + noseAmount.Rating + bodyAmount.Rating + mouthAmount.Rating + gearAmount.Rating;
                                    int count = headAmount.Count * eyeAmount.Count * noseAmount.Count * bodyAmount.Count * mouthAmount.Count * gearAmount.Count;
                                    if (!ratingToAmount.TryAdd(rating, count)) ratingToAmount[rating] += count;
                                }
                            }
                        }
                    }
                }
            }

            return ratingToAmount.ToDictionary(kvp => kvp.Key, kvp => kvp.Value * 100f / totalNumber);
        }
        
        private CreatureData GetRandom() 
            => new(GetRandomName(),
                GetRandomPart(CreatureComponentType.Mouth),
                GetRandomPart(CreatureComponentType.Eye),
                GetRandomPart(CreatureComponentType.Nose),
                GetRandomPart(CreatureComponentType.Body), 
                GetRandomPart(CreatureComponentType.Head), 
                GetRandomPart(CreatureComponentType.Gear),
                GetRandomColor());

        #region Utils

        private Part GetRandomPart(CreatureComponentType type)
            => _allParts[type][Random.Range(0, _allParts[type].Length)].part;
        private string GetRandomName() => names[Random.Range(0, names.Count)];
        private Material GetRandomColor() => colors[Random.Range(0, colors.Count)];
        
        public static CreatureData GetRandomCreature() => _instance.GetRandom();

        #endregion
    }
}