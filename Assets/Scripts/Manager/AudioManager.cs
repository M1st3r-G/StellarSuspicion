using System;
using Data;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Manager
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The Default Effect Source")] 
        private AudioSource effectSrc;
        [SerializeField] [Tooltip("The Music Src")] 
        private AudioSource musicSrc;
        [SerializeField] [Tooltip("The FuseBox Humming")]
        private AudioSource ambienceSrc;

        [Header("AudioClips")]
        [SerializeField] private AudioClipsContainer[] clips;

        public const string MasterVolumeKey = "MasterVolume";
        public const string MusicVolumeKey = "MusicVolume";
        public const string AmbienceVolumeKey = "AmbienceVolume";
        public const string EffectVolumeKey = "EffectVolume";
        
        public static AudioManager Instance { get; private set; }

        #region SetUp

        private void Awake()
        {
            if (Instance is not null)
            {
                Debug.LogWarning("There are multipla AudioManager");
                Destroy(this);
                return;
            }
            
            Instance = this;
        }
        
        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #endregion
        
        #region Music
        
        public static void StartStopMusic(bool play)
        {
            if (play) Instance.musicSrc.Play();
            else Instance.musicSrc.Pause();
        }
        
        #endregion
        
        #region EffectManagement

        /// <summary>
        /// Plays a Clip of the given SoundEffect and returns its length in seconds
        /// </summary>
        /// <param name="effect">The ClipType to Play</param>
        /// <returns>The length of the played Clip</returns>
        public float PlayEffect(AudioEffect effect)
        {
            foreach (AudioClipsContainer cnt in clips)
            {
                if (cnt.Type != effect) continue;
                AudioClip clip = cnt.GetClip();
                if (clip is null)
                {
                    Debug.LogError($"Noch keine Sounds für {effect} sind importiert");
                    return -1f;
                }
                
                effectSrc.PlayOneShot(clip);
                return clip.length;
            }
            
            Debug.LogError($"Sound with {effect} Identifier not found");
            return -1f;
        }
        
        [Serializable]
        private struct AudioClipsContainer
        {
            public string name;
            [SerializeField] private AudioEffect type;
            [SerializeField] private AudioClip[] clips;

            public AudioEffect Type => type;
            
            public AudioClip GetClip()
            {
                return clips.Length == 0 ? null : clips[Random.Range(0, clips.Length)];
            }
        }

        #endregion
    }
}