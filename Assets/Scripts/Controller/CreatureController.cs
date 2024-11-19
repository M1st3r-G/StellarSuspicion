using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class CreatureController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("This is the Image, later Containing the creature's Mouth")]
        [SerializeField] private Image mouth;
        [Tooltip("This is the Image, later Containing the creature's Eyes")]
        [SerializeField] private Image eyes;
        
        public void SetToCreature(CreatureData creature)
        {
            mouth.sprite = creature.Mouth;
            eyes.sprite = creature.Eyes;
            name = creature.Name;
        }

        public void ResetCreature()
        {
            mouth.sprite = null;
            eyes.sprite = null;
            name = "Default";
        }
    }
}
