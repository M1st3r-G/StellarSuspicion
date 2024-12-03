using System.Collections.Generic;
using UnityEngine;

namespace Data.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueQuestion", menuName = "Dialogue/Question")]
    public class DialogueQuestionAsset : ScriptableObject
    {
        [SerializeField] private string question;

        [SerializeField] private List<Answer> answers;

        [System.Serializable]
        private struct Answer
        {
            public string name;
            [Range(-1, 1)] public int rating;
        }
    }
}