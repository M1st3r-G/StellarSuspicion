using UnityEngine;

namespace Data
{
    public readonly struct CreatureVoiceLine
    {
        public readonly AudioClip Hello;
        private readonly AudioClip[] _sentences;
        private readonly AudioClip _death;
        private readonly AudioClip _thanks;
        private readonly AudioClip _kill;

        public CreatureVoiceLine(AudioClip hello, AudioClip[] sentence, AudioClip thanks, AudioClip kill, AudioClip death)
        {
            Hello = hello;
            _sentences = sentence;
            _thanks = thanks;
            _kill = kill;
            _death = death;
        }

        public AudioClip GetSentence(int index) => _sentences[index];
        
        public AudioClip GetResolution(CreatureAction action, bool success)
        {
            if (action is CreatureAction.Die) return _death;
            return success ? _thanks : _kill;
        }
    }
}