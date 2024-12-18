﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueQuestion", menuName = "Dialogue/Question")]
    public class DialogueQuestionAsset : ScriptableObject
    {
        [SerializeField] private string question;
        public string Question => question;
            
        [SerializeField] private List<AnswerContainer> answers;
        
        public (string, int) GetAnswer(int currentGoodness)
        {
            AnswerContainer[] set = (currentGoodness switch
            {
                1 => answers.Where(a => a.rating > 0),
                -1 => answers.Where(a => a.rating < 0),
                _ => answers
            }).ToArray();

            AnswerContainer ret = set[Random.Range(0, set.Length)];
            
            return (ret.name, ret.rating);
        }

        [Serializable]
        private struct AnswerContainer
        {
            public string name;
            [Range(-1, 1)] public int rating;
        }
    }
}