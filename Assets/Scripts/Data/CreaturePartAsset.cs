using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CreaturePart", menuName = "Data", order = 1)]
    public class CreaturePartAsset : ScriptableObject
    {
        public Part part;
    }
}