using Data;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class CreatureController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("This is the CanvasGroup for this creature")]
        private CanvasGroup canvasGroup;

        
        [Header("MonsterParts")]
        [SerializeField] [Tooltip("This is the Image, later Containing the creature's Mouth")]
        private Image mouth;
        
        [SerializeField] [Tooltip("This is the Image, later Containing the creature's Eyes")]
        private Image eyes;
        
        [SerializeField] [Tooltip("This is the Image, later Containing the creature's Eyes")]
        private Image nose;
        
        [SerializeField] [Tooltip("This is the Image, later Containing the creature's Eyes")]
        private Image body;

        [SerializeField] [Tooltip("This is the Image, later Containing the creature's Eyes")]
        private Image head;

        [SerializeField] [Tooltip("This is the Image, later Containing the creature's Eyes")]
        private Image headGear;

        public CreatureData? CurrentCreature
        {
            get => _currentCreature;
            private set
            {
                _currentCreature = value;
                mouth.sprite = _currentCreature?.Mouth;
                eyes.sprite = _currentCreature?.Eyes;
                nose.sprite = _currentCreature?.Nose;
                body.sprite = _currentCreature?.Body;
                head.sprite = _currentCreature?.Head;
                headGear.sprite = _currentCreature?.Gear;
            
                body.material = _currentCreature?.Color;
                head.material = _currentCreature?.Color;
            }
        }
        private CreatureData? _currentCreature;

        private bool IsVisible => canvasGroup.alpha > 0.5f;
        public bool ShowingCreature => IsVisible && CurrentCreature is not null;

        private void Awake() => SetVisibility(false);
        public void SetVisibility(bool on) => canvasGroup.alpha = on ? 1f : 0f;

        public void SetToCreature(CreatureData creature)
        {
            if (!IsVisible) canvasGroup.alpha = 1f;
            name = creature.Name;
            UIManager.Dialogue.SetText($"Glorb blorb bla: {name}!");
            CurrentCreature = creature;
        }

        private void ResetCreature()
        {
            name = "Default";
            CurrentCreature = null;
            canvasGroup.alpha = 0f;
        }

        public void Clear(AcceptMode acceptMode)
        {
            //Wait
            //Stuff
            ResetCreature();
        }
    }
}
