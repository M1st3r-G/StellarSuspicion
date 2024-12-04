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
        private CreatureVoiceController Voice { get; set; }
        public int RatingFromQuestions { get; private set; }
        
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

        public bool HasCreature => _currentCreature is not null;
        private bool IsWaiting => HasCreature && transform.localPosition.x < 0.1f;
        
        #endregion
        
        public void Awake()
        {
            _anim = GetComponent<Animator>();
            Voice = GetComponent<CreatureVoiceController>();
            _creatureRenderer = GetComponent<CreatureRenderer>();
        }

        public void SetToCreature(CreatureData creature)
        {
            if (IsWaiting) return;
            
            name = creature.Name;
            RatingFromQuestions = 0;
            UIManager.Dialogue.ShowGreeting();
            CurrentCreature = creature;
            
            _anim.Play("Enter");
            Voice.StartSteps();
            Voice.PlayerHello();
            
            GameManager.Mic.SetInteractionTo(true); //TODO only when Neutral? 
            Debug.Log($"This Creature is currently {this.IsGood()}");
        }

        public void OnFinishedMovement() => Voice.StopSounds();

        public void ResetCreature()
        {
            Debug.Assert(CurrentCreature != null, nameof(CurrentCreature) + " != null");
            
            name = "Default";
            RatingFromQuestions = 0;
            
            GameManager.RateCreature(this);
            CurrentCreature = null;
            
            Voice.StopSounds();
            
            GameManager.Mic.SetInteractionTo(false);
        }

        public void Clear(CreatureAction exitAction, bool success)
        {
            _anim.Play(exitAction is CreatureAction.Exit ? "Exit" : "Drop");

            if (exitAction is CreatureAction.Exit) Voice.StartSteps();
            Voice.PlayResolution(exitAction, success);
        }

        public void AnswerQuestion(int index, int rating)
        {
            RatingFromQuestions += rating;
            Debug.Log($"This Creature is currently {this.IsGood()}");
            Voice.PlaySentence(index);
        }
    }
}
