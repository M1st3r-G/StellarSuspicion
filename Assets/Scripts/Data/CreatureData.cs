using UnityEngine;

namespace Data
{
    public struct CreatureData
    {
        public string Name;
        public Sprite Mouth;
        public Sprite Eyes;

        public CreatureData(string name, Sprite mouth, Sprite eyes)
        {
            Name = name;
            Mouth = mouth;
            Eyes = eyes;
        }
        
        // Reason
    }
}