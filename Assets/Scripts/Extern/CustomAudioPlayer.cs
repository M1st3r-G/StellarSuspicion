using System.Collections;
using UnityEngine;

namespace Extern
{
    [RequireComponent(typeof(AudioSource))]
    public class CustomAudioPlayer : MonoBehaviour
    {
        private AudioSource _src;
        private Coroutine _currentAudioLoop;
        private const float Puffer = 0.125f;
        
        private void Awake()
        {
            _src = GetComponent<AudioSource>();
        }

        public void Play(AudioClipsContainer clips, bool looping)
        {
            if (!looping) _src.PlayOneShot(clips.GetClip());
            else
            {
                if (_currentAudioLoop is not null) StopCoroutine(_currentAudioLoop);
                _currentAudioLoop = StartCoroutine(PlayAsLoop(clips));
            }
        }

        public void StopLoop()
        {
            if (_currentAudioLoop is null) return;
            StopCoroutine(_currentAudioLoop);
            _currentAudioLoop = null;
        }

        private IEnumerator PlayAsLoop(AudioClipsContainer clips)
        {
            while (true)
            {
                AudioClip currentClip = clips.GetClip();
                _src.PlayOneShot(currentClip);
                yield return new WaitForSeconds(currentClip.length + Puffer);
            }
        }
    }
}