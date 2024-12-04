using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Dialogue;
using Extern;
using Manager;
using TMPro;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Controller.UI
{
    [RequireComponent(typeof(CanvasGroup))]
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

        [SerializeField] private List<TextMeshProUGUI> optionTexts;
        
        private CanvasGroup _myGroup;
        
        private void Awake()
        {
            _myGroup = GetComponent<CanvasGroup>();
            _myGroup.SetGroupActive(false);
        }

        public void ShowGreeting()
        {
            SetText(greetings.GetLine());
            ResetOptions();
        }

        public void ShowResolution(CreatureAction interaction, bool success)
        {
            if (interaction is CreatureAction.Exit) SetText(success ? goodExit.GetLine() : evilExit.GetLine());
            else SetText(success ? evilEject.GetLine() : goodEject.GetLine());
        }

        private void SetText(string text) => textBox.text = text;

        public void ShowQuestionOptions()
        {
            _myGroup.SetGroupActive(true);

            for (int i = 0; i < questions.Count; i++)
            {
                DialogueQuestionAsset qAsset = questions[i];
                optionTexts[i].text = qAsset.Question;
            }
        }

        private void ResetOptions()
        {
            foreach (TextMeshProUGUI text in optionTexts) text.transform.parent.parent.gameObject.SetActive(true);
        }
        
        public void ButtonPressed(int index)
        {
            Debug.Assert(GameManager.Creature.CurrentCreature is not null, "GameManager.Creature.CurrentCreature != null");

            (string content, int rating) = questions[index].GetAnswer(GameManager.Creature.GetGoodness(out _));
            SetText(content);
            
            optionTexts[index].transform.parent.parent.gameObject.SetActive(false);
            _myGroup.SetGroupActive(false);
            
            GameManager.Creature.AnswerQuestion(index, rating);
            if (optionTexts.Any(t => t.transform.parent.parent.gameObject.activeSelf)) GameManager.Mic.SetInteractionTo(true);
        }
    }
}
