using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Data
{
    public readonly struct CreatureData
    {
        public readonly string Name;
        private readonly PartBundle _parts;

        public Sprite GetSprite(CreatureComponentType type) => _parts[type].sprite;
        public Material Color { get; }
        public CreatureVoiceLine VoiceLine { get; }

        public CreatureData(string name, PartBundle bundle, Material color, CreatureVoiceLine randomVoice)
        {
            Name = name;
            Color = color;
            _parts = bundle;
            VoiceLine = randomVoice;
        }
        
        // Reason
        public IEnumerable<Part> GetAllParts() => _parts.AllParts;
    }
}