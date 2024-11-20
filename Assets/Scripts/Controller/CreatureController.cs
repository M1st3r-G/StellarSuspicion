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
        
        public void SetToCreature(CreatureData creature)
        {
            mouth.sprite = creature.Mouth;
            eyes.sprite = creature.Eyes;
            nose.sprite = creature.Nose;
            name = creature.Name;
        }

        public void ResetCreature()
        {
            mouth.sprite = null;
            eyes.sprite = null;
            nose.sprite = null;
            name = "Default";
        }
    }
}
