using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public struct CreatureVoiceType
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

    public readonly struct CreatureVoiceLine
    {
        public readonly AudioClip Hello;
        public readonly AudioClip Sentence;
        public readonly AudioClip Thanks;
        public readonly AudioClip Kill;
        public readonly AudioClip Death;

        public CreatureVoiceLine(AudioClip hello, AudioClip sentence, AudioClip thanks, AudioClip kill, AudioClip death)
        {
            Hello = hello;
            Sentence = sentence;
            Thanks = thanks;
            Kill = kill;
            Death = death;
        }
    }
}