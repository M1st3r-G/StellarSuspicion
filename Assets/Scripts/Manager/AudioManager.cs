using System;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Reference to EffectSource component")]
        [SerializeField]private AudioSource effectSource;
        [Tooltip("reference to MusicSource component")]
        [SerializeField]private AudioSource musicSource;
        
        [Header("Parameters")]
        [Tooltip("List of audio clips")]
        public List<AudioClip> audioClips = new();
    
    
        #region Setup

        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of CameraManager");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #endregion

        public static void PlayAudio(int index)
        {
            Instance.effectSource.PlayOneShot(Instance.audioClips[index]);
        }

        public static void StartStopMusic(bool isPlaying)
        {
            Instance.musicSource.enabled = !isPlaying;
        }
    }
}
