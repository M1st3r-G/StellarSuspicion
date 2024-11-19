using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CreaturePart", menuName = "Data", order = 1)]
    public class CreaturePartAsset : ScriptableObject
    {
        public Part part;
    }
    
    [Serializable]
    public struct Part
    {
        public Sprite sprite;
        public CreatureComponentType type;
        public bool isGood;
    }
}