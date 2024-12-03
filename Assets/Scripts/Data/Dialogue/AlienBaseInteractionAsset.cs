using System.Collections.Generic;
using UnityEngine;

namespace Data.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/BaseInteract")]
    public class AlienBaseInteractionAsset : ScriptableObject
    {
        [SerializeField] protected List<string> answers;

        public virtual string GetLine() => answers[Random.Range(0, answers.Count)];
    }
}