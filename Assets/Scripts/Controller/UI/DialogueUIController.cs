using System.Collections.Generic;
using Data;
using Data.Dialogue;
using TMPro;
using UnityEngine;

namespace Controller.UI
{
    public class DialogueUIController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] [Tooltip("The Questions to Ask")]
        private List<DialogueQuestionAsset> questions;

        [SerializeField] private GreetingInteraction greetings;

        [SerializeField] private AlienBaseInteractionAsset goodExit;
        [SerializeField] private AlienBaseInteractionAsset evilExit;
        [SerializeField] private AlienBaseInteractionAsset goodEject;
        [SerializeField] private AlienBaseInteractionAsset evilEject;

        [SerializeField] [Tooltip("This is the Text Box for Dialogue")]
        private TextMeshProUGUI textBox;
        
        public void ShowGreeting() => SetText(greetings.GetLine());
        
        public void ShowResolution(CreatureAction interaction, bool success)
        {
            if (interaction is CreatureAction.Exit) SetText(success ? goodExit.GetLine() : evilExit.GetLine());
            else SetText(success ? evilEject.GetLine() : goodEject.GetLine());
        }

        private void SetText(string text) => textBox.text = text;

        public static void ShowQuestionOptions()
        {
            Debug.LogWarning("Ask A Question");
        }
    }
}
