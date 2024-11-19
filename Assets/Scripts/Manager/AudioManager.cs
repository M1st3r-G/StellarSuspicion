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
        public List<AudioClip> audioClips = new List<AudioClip>();
    
    
        #region Setup

        public static AudioManager _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("More than one instance of CameraManager");
                Destroy(gameObject);
                return;
            }
            Debug.Log("Camera Manager");
            _instance = this;
        }

        public void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }

        #endregion

        public static void PlayAudio(int index)
        {
            _instance.effectSource.PlayOneShot(_instance.audioClips[index]);
        }

        public static void StartStopMusic(bool isPlaying)
        {
            _instance.musicSource.enabled = !isPlaying;
        }
    }
}
