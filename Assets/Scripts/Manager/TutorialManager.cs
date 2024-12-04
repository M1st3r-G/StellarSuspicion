using System.Collections;
using UnityEngine;

namespace Manager
{
    public class TutorialManager : MonoBehaviour
    {
        private TutorialFlag _lastFlag;
        private bool _isInTutorial;

        private static TutorialManager Instance { get; set; }
        
        public enum TutorialFlag
        {
            None = 0,
            SatDown,
            AnsweredQuestion,
            GeneratorInteracted,
            ZoomedOutOfHelp,
            PressedButtonKill,
            PressedButtonExit
        }

        private void Awake()
        {
            if (Instance is not null)
            {
                Debug.LogWarning("More than one instance of TutorialManager!");
                Destroy(this);
                return;
            }
            
            Instance = this;
        }

        public static void StartTutorial() => Instance.InnerStartTutorial();
        private void InnerStartTutorial()
        {
            if (_isInTutorial) return;
            
            _isInTutorial = true;
            StartCoroutine(TutorialRoutine());
        }

        public static  void SetFlag(TutorialFlag type)
        {
            if (Instance._isInTutorial) Instance._lastFlag = type;
        }

        private IEnumerator TutorialRoutine()
        {
            yield return new WaitUntil(() => true);
            _isInTutorial = false;
        }
    }
}
