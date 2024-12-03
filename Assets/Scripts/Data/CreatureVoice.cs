using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public struct CreatureVoiceType
    {
        public string name;
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
        private readonly AudioClip _death;
        private readonly AudioClip _thanks;
        private readonly AudioClip _kill;

        public CreatureVoiceLine(AudioClip hello, AudioClip sentence, AudioClip thanks, AudioClip kill, AudioClip death)
        {
            Hello = hello;
            Sentence = sentence;
            _thanks = thanks;
            _kill = kill;
            _death = death;
        }

        public AudioClip GetResolution(CreatureAction action, bool success)
        {
            if (action is CreatureAction.Die) return _death;
            return success ? _thanks : _kill;
        }
    }
}