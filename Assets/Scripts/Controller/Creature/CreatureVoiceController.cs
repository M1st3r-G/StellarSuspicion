using Data;
using UnityEngine;

namespace Controller.Creature
{
    public class CreatureVoiceController : MonoBehaviour
    {
        [Header("References")] 
        
        [SerializeField] [Tooltip("This is the Walking Loop")]
        private AudioSource creatureSteps;
        
        [SerializeField] [Tooltip("This is the Voice Source")]
        private AudioSource creatureVoice;

        private CreatureVoiceLine? _currentVoiceLine;
        
        public void PlayerHello() => creatureVoice.PlayOneShot(_currentVoiceLine?.Hello);
        public void PlaySentence(int index) => creatureVoice.PlayOneShot(_currentVoiceLine?.GetSentence(index));
        
        public void StartSteps()
        {
            //Look for offset Points
            //creatureSteps.time = Random.Range(0f, 5f);
            creatureSteps.Play();
        }

        public void StopSounds() => creatureSteps.Stop();

        public void SetVoice(CreatureVoiceLine? voiceLine) => _currentVoiceLine = voiceLine;

        public void PlayResolution(CreatureAction action, bool success)
        {
            // Die and Exit (Thanks and Kill)
            creatureVoice.PlayOneShot(_currentVoiceLine?.GetResolution(action, success));
        }
    }
}