using System;
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
}