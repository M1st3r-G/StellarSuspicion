using System.Collections;
using System.Collections.Generic;
using Controller;
using Controller.Actors.Interactable.Buttons;
using Controller.Actors.Interactable.Table;
using UnityEngine;

namespace Manager
{
    public class TutorialManager : MonoBehaviour
    {
        private TutorialFlag _lastFlag;
        private bool _isInTutorial;

        [SerializeField] private List<string> tutorialContent;
        [SerializeField] private FuseBoxController fuseBox;
        [SerializeField] private ScreenInteractController screen;
        [SerializeField] private ButtonKillInteract killButton;
        [SerializeField] private ButtonEnterInteract exitButton;

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
            
            StartTutorial();
        }

        private void StartTutorial()
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
            UIManager.Dialogue.ShowTutorial(tutorialContent[0]);
            
            GameManager.Mic.SetInteractionTo(true);
            GameManager.Mic.TutorialGlow();
            // Todo SHOW DEFAULT ANSWER TUTORIAL
            
            // Scene 2
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.AnsweredQuestion);
            UIManager.Dialogue.ShowTutorial(tutorialContent[1]);
            
            yield return new WaitForSeconds(1f);
            fuseBox.TriggerPowerEvent(false);
            
            // Scene 3
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.GeneratorInteracted);
            UIManager.Dialogue.ShowTutorial(tutorialContent[2]);
            
            // Scene 4
            yield return new WaitForSeconds(3f);
            UIManager.Dialogue.ShowTutorial(tutorialContent[3]);
            screen.TutorialGlow();
            
            // Scene 5
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.ZoomedOutOfHelp);
            UIManager.Dialogue.ShowTutorial(tutorialContent[4]);
            
            killButton.SetInteractionTo(true);
            exitButton.SetInteractionTo(true);
            
            //Last Scene
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.PressedButtonKill or TutorialFlag.PressedButtonExit);
            UIManager.Dialogue.ShowTutorial(tutorialContent[_lastFlag is TutorialFlag.PressedButtonKill ? 5 : 6]);
            
            _isInTutorial = false;
        }
    }
}
