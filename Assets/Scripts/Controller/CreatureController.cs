using System.Runtime.ConstrainedExecution;
using Controller.UI;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class CreatureController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("This is the Dialogue Controller for this creature")]
        private DialogueUIController dialogue;

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
        
        public void SetToCreature(CreatureData creature)
        {
            name = creature.Name;
            dialogue.SetText($"Glorb blorb bla: {name}!");
            
            mouth.sprite = creature.Mouth;
            eyes.sprite = creature.Eyes;
            nose.sprite = creature.Nose;
            body.sprite = creature.Body;
            head.sprite = creature.Head;
            headGear.sprite = creature.Gear;
            
            body.material = creature.Color;
            head.material = creature.Color;
        }

        public void ResetCreature()
        {
            name = "Default";
            dialogue.SetText("");

            mouth.sprite = eyes.sprite = nose.sprite = body.sprite = head.sprite = headGear.sprite = null;
            body.material =  head.material = null;
        }
    }
}
