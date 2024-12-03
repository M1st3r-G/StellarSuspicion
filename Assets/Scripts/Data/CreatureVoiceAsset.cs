using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewVoice", menuName = "Data/Voice")]
    public class CreatureVoiceAsset : ScriptableObject
    {
        [SerializeField] private List<AudioClip> hello;
        [SerializeField] private List<AudioClip> sentence;
        [SerializeField] private List<AudioClip> thanks;
        [SerializeField] private List<AudioClip> kill;
        [SerializeField] private List<AudioClip> death;
        
        public CreatureVoiceLine GetVoiceLine() =>
            new(
                hello[Random.Range(0, hello.Count)],
                sentence[Random.Range(0, sentence.Count)],
                thanks[Random.Range(0, thanks.Count)],
                kill[Random.Range(0, kill.Count)],
                death[Random.Range(0, death.Count)]
            );
        
        public int GetPossibilities() => hello.Count * sentence.Count * thanks.Count * kill.Count * death.Count;
    }
}