using Data;
using UnityEngine;

namespace Manager
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] [Tooltip("The Effect Container Asset Prefab")]
        private GameObject effectPrefab;
        [SerializeField] [Tooltip("The Music Src")]
        private AudioSource musicSrc;
        [Header("AudioClips")]
        [SerializeField] [Tooltip("The Sound Src")]
        private AudioClipsContainer[] clips;
        
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
        
        #region Effects

        /// <summary>
        /// Plays a Clip of the given SoundEffect and returns its length in seconds
        /// </summary>
        /// <param name="effect">The ClipType to Play</param>
        /// <param name="position">The Position where the Clip should be played</param>
        /// <returns>The length of the played Clip</returns>
        public static void PlayEffect(AudioEffect effect, Vector3 position) => Instance.InnerPlayEffect(effect, position);

        private void InnerPlayEffect(AudioEffect effect, Vector3 position)
        {
            foreach (AudioClipsContainer cnt in clips)
            {
                if (cnt.Type != effect) continue;
                AudioClip clip = cnt.GetClip();
                if (clip is null)
                {
                    Debug.LogError($"Noch keine Sounds für {effect} sind importiert");
                    return;
                }
                
                GameObject tmp = Instantiate(effectPrefab, position, Quaternion.identity);
                tmp.GetComponent<AudioSource>().clip = cnt.GetClip();
            }
            
            Debug.LogError($"Sound with {effect} Identifier not found");
        }
        

        #endregion
    }
}