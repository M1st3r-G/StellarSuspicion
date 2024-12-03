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
        
        public void PlayerInteraction(CreatureAction action)
        {
            Debug.Assert(_currentVoiceLine != null);
            // Only Hello and Talk
            creatureVoice.PlayOneShot(action is CreatureAction.Hello
                ? _currentVoiceLine?.Hello
                : _currentVoiceLine?.Sentence);
        }
        
        public void StartSteps() => creatureSteps.Stop();
        public void StopSounds() => creatureSteps.Stop();

        public void SetVoice(CreatureVoiceLine? voiceLine) => _currentVoiceLine = voiceLine;

        public void PlayResolution(CreatureAction action, bool success)
        {
            // Die and Exit (Thanks and Kill)
            creatureVoice.PlayOneShot(_currentVoiceLine?.GetResolution(action, success));
        }
    }
}