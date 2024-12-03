using Data;
using Extern;
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
        public CreatureVoiceController Voice { get; private set; }

        public CreatureData? CurrentCreature
        {
            get => _currentCreature;
            private set
            {
                _currentCreature = value;
                _creatureRenderer.SetRenderer(_currentCreature);
                Voice.SetVoice(_currentCreature?.VoiceLine);
            }
        }

        private bool IsVisible => _creatureRenderer.IsVisible;

        #endregion
        
        public void Awake()
        {
            _anim = GetComponent<Animator>();
            Voice = GetComponent<CreatureVoiceController>();
            _creatureRenderer = GetComponent<CreatureRenderer>();
            _creatureRenderer.SetVisibility(false);
        }

        public void SetToCreature(CreatureData creature)
        {
            if (!IsVisible) _creatureRenderer.SetVisibility(true);
            name = creature.Name;
            UIManager.Dialogue.ShowGreeting();
            CurrentCreature = creature;
            
            _anim.Play("Enter");
            Voice.StartSteps();
            Voice.PlayerHello();
            
            GameManager.Mic.SetInteractionTo(true); //TODO only when Neutral? 
            Debug.Log($"This Creature is {CurrentCreature?.IsGood()}");
        }

        public void OnFinishedMovement() => Voice.StopSounds();

        public void ResetCreature()
        {
            Debug.Assert(CurrentCreature != null, nameof(CurrentCreature) + " != null");
            
            name = "Default";
 
            GameManager.RateCreature(CurrentCreature.Value);
            CurrentCreature = null;
            _creatureRenderer.SetVisibility(false);
            
            Voice.StopSounds();
            
            GameManager.Mic.SetInteractionTo(false);
        }

        public void Clear(CreatureAction exitAction, bool success)
        {
            _anim.Play(exitAction is CreatureAction.Exit ? "Exit" : "Drop");

            if (exitAction is CreatureAction.Exit) Voice.StartSteps();
            Voice.PlayResolution(exitAction, success);
        }

        public void SetVisibility(bool on) => _creatureRenderer.SetVisibility(on);
    }
}
