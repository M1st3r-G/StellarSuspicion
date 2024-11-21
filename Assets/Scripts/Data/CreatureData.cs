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
        public Sprite Nose => _parts.FirstOrDefault(p => p.type == CreatureComponentType.Nose).sprite;
        public Sprite Body => _parts.FirstOrDefault(p=> p.type == CreatureComponentType.Body).sprite;
        public Sprite Head => _parts.FirstOrDefault(p=> p.type == CreatureComponentType.Head).sprite;
        public Material Color { get; }


        public CreatureData(string name, Part mouth, Part eyes, Part nose, Part body, Part head, Material color)
        {
            Name = name;
            Color = color;

            Debug.Assert(mouth.type == CreatureComponentType.Mouth);
            Debug.Assert(eyes.type == CreatureComponentType.Eye);
            Debug.Assert(nose.type == CreatureComponentType.Nose);
            Debug.Assert(body.type == CreatureComponentType.Body);
            Debug.Assert(head.type == CreatureComponentType.Head);
            
            _parts = new List<Part>
            {
                mouth,
                eyes,
                nose, 
                body,
                head
            };
        }
        
        // Reason
        public Part GetPart(CreatureComponentType type) => _parts.FirstOrDefault(p => p.type == type);
        public IEnumerable<Part> GetAllParts() => _parts.ToArray();
    }
}