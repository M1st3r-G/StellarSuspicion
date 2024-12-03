using System.Collections.Generic;
using UnityEngine;

namespace Data.Dialogue
{
    [CreateAssetMenu(fileName = "GreetingInteraction", menuName = "Dialogue/GreetingInteraction")]
    public class GreetingInteraction : AlienBaseInteractionAsset
    {
        [SerializeField] private List<string> greetingNames;
        
        public override string GetLine() => base.GetLine().Replace("<name>", greetingNames[Random.Range(0, greetingNames.Count)]);
    }
}