using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CreaturePart", menuName = "Data/CreaturePart")]
    public class CreaturePartAsset : ScriptableObject
    {
        public Part part;
    }
}