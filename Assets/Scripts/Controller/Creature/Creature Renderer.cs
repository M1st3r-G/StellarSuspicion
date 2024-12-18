﻿using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Controller.Creature
{
    public class CreatureRenderer : MonoBehaviour
    {
        [Header("MonsterParts")]
        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Mouth")]
        private SpriteRenderer mouth;
        
        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Eyes")]
        private SpriteRenderer eyes;
        
        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Nose")]
        private SpriteRenderer nose;
        
        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Body")]
        private SpriteRenderer body;

        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Head")]
        private SpriteRenderer head;

        [SerializeField] [Tooltip("This is the Sprite, later Containing the creature's Gear")]
        private SpriteRenderer headGear;

        private Dictionary<CreatureComponentType, SpriteRenderer> _monsterPartRenderer;
        
        public void SetRenderer(CreatureData? data)
        {
            foreach (CreatureComponentType type in Enum.GetValues(typeof(CreatureComponentType)))
                _monsterPartRenderer[type].sprite = data?.GetSprite(type);
                
            body.material = data?.Color; 
            head.material = data?.Color;
        }

        private void Awake()
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
        }
    }
}