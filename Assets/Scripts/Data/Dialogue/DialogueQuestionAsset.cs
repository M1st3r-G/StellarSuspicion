using System.Collections.Generic;
using UnityEngine;

namespace Data.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueQuestion", menuName = "Dialogue/Question")]
    public class DialogueQuestionAsset : ScriptableObject
    {
        [SerializeField] private string question;
        public string Question => question;
            
        [SerializeField] private List<AnswerContainer> answers;
        public string Answer => answers[Random.Range(0, answers.Count)].name;

        [System.Serializable]
        private struct AnswerContainer
        {
            public string name;
            [Range(-1, 1)] public int rating;
        }

    }
}