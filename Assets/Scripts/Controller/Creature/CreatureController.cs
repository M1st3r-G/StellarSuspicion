using Data;
using Manager;
using UnityEngine;

namespace Controller.Creature
{
    [RequireComponent(typeof(Animator), typeof(CreatureRenderer), typeof(CreatureVoiceController))]
    public class CreatureController : MonoBehaviour
    {
        #region Variables
        
        private CreatureData? _currentCreature;
        private CreatureRenderer _creatureRenderer;
        private Animator _anim;
        private CreatureVoiceController _creatureVoice;

        public CreatureData? CurrentCreature
        {
            get => _currentCreature;
            private set
            {
                _currentCreature = value;
                _creatureRenderer.SetRenderer(_currentCreature);
                _creatureVoice.SetVoice(_currentCreature?.VoiceType.GetVoiceLine());
            }
        }

        private bool IsVisible => _creatureRenderer.IsVisible;

        #endregion
        
        public void Awake()
        {
            _anim = GetComponent<Animator>();
            _creatureVoice = GetComponent<CreatureVoiceController>();
            _creatureRenderer = GetComponent<CreatureRenderer>();
            _creatureRenderer.SetVisibility(false);
        }

        public void SetToCreature(CreatureData creature)
        {
            if (!IsVisible) _creatureRenderer.SetVisibility(true);
            name = creature.Name;
            UIManager.Dialogue.ShowInteraction(CreatureAction.Hello, _currentCreature);
            CurrentCreature = creature;
            
            _anim.Play("Enter");
            _creatureVoice.StartSteps();
            _creatureVoice.PlayerInteraction(CreatureAction.Hello);
        }

        public void OnFinishedMovement() => _creatureVoice.StopSounds();

        public void ResetCreature()
        {
            name = "Default";
            CurrentCreature = null;
            _creatureRenderer.SetVisibility(false);
            
            _creatureVoice.StopSounds();
        }

        public void Clear(CreatureAction exitAction, bool success)
        {
            _anim.Play(exitAction is CreatureAction.Exit ? "Exit" : "Drop");

            if (exitAction is CreatureAction.Exit) _creatureVoice.StartSteps();
            _creatureVoice.PlayResolution(exitAction, success);
        }

        public void SetVisibility(bool on) => _creatureRenderer.SetVisibility(on);
    }
}
