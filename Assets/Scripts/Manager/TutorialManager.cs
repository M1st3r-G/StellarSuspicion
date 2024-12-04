using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class TutorialManager : MonoBehaviour
    {
        private TutorialFlag _lastFlag;
        private bool _isInTutorial;

        private List<string> _tutorialContent;
        
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
            // Scene 1
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.SatDown);
            UIManager.Dialogue.ShowTutorial(_tutorialContent[0]);
            
            GameManager.Mic.SetInteractionTo(true);
            GameManager.Mic.TutorialGlow();
            // Todo SHOW DEFAULT ANSWER TUTORIAL
            
            // Scene 2
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.AnsweredQuestion);
            UIManager.Dialogue.ShowTutorial(_tutorialContent[1]);
            
            yield return new WaitForSeconds(1f);
            GameManager.FuseBox.TriggerPowerEvent(false);
            
            // Scene 3
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.GeneratorInteracted);
            
            
            
            _isInTutorial = false;
        }
    }
}
