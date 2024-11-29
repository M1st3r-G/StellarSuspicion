using System.Collections;
using System.Collections.Generic;
using Extern;
using UnityEngine;

namespace Manager
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] [Tooltip("The Music Src")]
        private AudioSource musicSrc;

        private const float Puffer = 0.125f;

        public const string MasterVolumeKey = "MasterVolume";
        public const string MusicVolumeKey = "MusicVolume";
        public const string AmbienceVolumeKey = "AmbienceVolume";
        public const string EffectVolumeKey = "EffectVolume";

        private static AudioManager Instance { get; set; }

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
        
        public static IEnumerator PlayAsLoop(AudioSource src, AudioClipsContainer clips)
        {
            while (true)
            {
                AudioClip currentClip = clips.GetClip();
                src.PlayOneShot(currentClip);
                yield return new WaitForSeconds(currentClip.length + Puffer);
            }
        }
    }
}