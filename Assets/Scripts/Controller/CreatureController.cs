using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Manager;
using UnityEngine;

namespace Controller
{
    public class CreatureController : MonoBehaviour
    {
        [Header("MonsterParts")]
        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Mouth")]
        private SpriteRenderer mouth;
        
        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Eyes")]
        private SpriteRenderer eyes;
        
        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Eyes")]
        private SpriteRenderer nose;
        
        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Eyes")]
        private SpriteRenderer body;

        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Eyes")]
        private SpriteRenderer head;

        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Eyes")]
        private SpriteRenderer headGear;

        private Dictionary<CreatureComponentType, SpriteRenderer> _monsterPartRenderer;

        public void Awake()
        {
            _monsterPartRenderer = new Dictionary<CreatureComponentType, SpriteRenderer>
            {
                { CreatureComponentType.Eye, eyes },
                { CreatureComponentType.Mouth, mouth },
                { CreatureComponentType.Nose, nose },
                { CreatureComponentType.Head, head },
                { CreatureComponentType.Body, body },
                { CreatureComponentType.Gear, headGear }
            };
            
            SetVisibility(false);
        }
        
        public CreatureData? CurrentCreature
        {
            get => _currentCreature;
            private set
            {
                _currentCreature = value;
                foreach (CreatureComponentType type in Enum.GetValues(typeof(CreatureComponentType)))
                    _monsterPartRenderer[type].sprite = _currentCreature?.GetSprite(type);
                
                //body.material = _currentCreature?.Color;
                //head.material = _currentCreature?.Color;
            }
        }
        private CreatureData? _currentCreature;

        private float Alpha
        {
            get => _monsterPartRenderer.First().Value.color.a;

            set
            {
                foreach (var (key, spriteRenderer) in _monsterPartRenderer)
                {
                    Color prevColor = spriteRenderer.color;
                    prevColor.a = value;
                    spriteRenderer.color = prevColor;
                }
            }
        }
        
        private bool IsVisible => Alpha > 0.5f;
        public bool ShowingCreature => IsVisible && CurrentCreature is not null;

        public void SetVisibility(bool on) => Alpha = on ? 1f : 0f;

        public void SetToCreature(CreatureData creature)
        {
            if (!IsVisible) Alpha = 1f;
            name = creature.Name;
            UIManager.Dialogue.SetText($"Glorb blorb bla: {name}!");
            CurrentCreature = creature;
        }

        private void ResetCreature()
        {
            name = "Default";
            CurrentCreature = null;
            Alpha = 0f;
        }

        public void Clear(AcceptMode acceptMode)
        {
            //Wait
            //Stuff
            ResetCreature();
        }
    }
}
