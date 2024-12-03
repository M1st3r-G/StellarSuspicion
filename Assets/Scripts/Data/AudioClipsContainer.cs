using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data
{
    [Serializable]
    public struct AudioClipsContainer
    {
        public string name;
        [SerializeField] private AudioEffect type;
        [SerializeField] private AudioClip[] clips;
        
        public AudioEffect Type => type;
        public AudioClip GetClip() => clips.Length == 0 ? null : clips[Random.Range(0, clips.Length)];
    }
}