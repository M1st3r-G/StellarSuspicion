using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class CreatureController : MonoBehaviour
    {
        [Header("References")]
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

        
        public void SetToCreature(CreatureData creature)
        {
            name = creature.Name;

            mouth.sprite = creature.Mouth;
            eyes.sprite = creature.Eyes;
            nose.sprite = creature.Nose;
            body.sprite = creature.Body;
            head.sprite = creature.Head;
            
            body.material = creature.Color;
            head.material = creature.Color;
            
        }

        public void ResetCreature()
        {
            name = "Default";
            
            mouth.sprite = null;
            eyes.sprite = null;
            nose.sprite = null;
            body.sprite = null;
            head.sprite = null;
            
            body.material = null;
            head.material = null;
        }
    }
}
