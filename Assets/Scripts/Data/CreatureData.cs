using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    public readonly struct CreatureData
    {
        public readonly string Name;
        private readonly List<Part> _parts;
        public Sprite Mouth => _parts.FirstOrDefault(p => p.type == CreatureComponentType.Mouth).sprite;
        public Sprite Eyes => _parts.FirstOrDefault(p => p.type == CreatureComponentType.Eye).sprite;

        public CreatureData(string name, Part mouth, Part eyes)
        {
            Name = name;
            
            Debug.Assert(mouth.type == CreatureComponentType.Mouth);
            Debug.Assert(eyes.type == CreatureComponentType.Eye);

            _parts = new List<Part>
            {
                mouth,
                eyes
            };
        }
        
        // Reason
        public Part GetPart(CreatureComponentType type) => _parts.FirstOrDefault(p => p.type == type);
        public Part[] GetAllParts() => _parts.ToArray();
    }
}