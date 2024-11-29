using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct Part
    {
        public Sprite sprite;
        public CreatureComponentType type;
        public int goodness;
    }

    public struct PartBundle
    {
        private readonly Dictionary<CreatureComponentType, Part> _parts;
        public PartBundle(Part mouth, Part eyes, Part nose, Part body, Part head, Part gear)
        {
            _parts = new Dictionary<CreatureComponentType, Part>
            {
                { CreatureComponentType.Mouth, mouth },
                { CreatureComponentType.Eye, eyes },
                { CreatureComponentType.Nose, nose },
                { CreatureComponentType.Body, body },
                { CreatureComponentType.Head, head },
                { CreatureComponentType.Gear, gear }
            };
        }
        
        public Part GetPart(CreatureComponentType type) => this[type];
        public Part this[CreatureComponentType type] => _parts[type];
        public Part[] AllParts => _parts.Values.ToArray();
    }
}