using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extern
{
    [Serializable]
    public struct AudioClipsContainer
    {
        [SerializeField] private AudioClip[] clips;
        public AudioClip GetClip() => clips.Length == 0 ? null : clips[Random.Range(0, clips.Length)];
    }
}