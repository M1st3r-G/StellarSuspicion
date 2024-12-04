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
        [SerializeField] private bool _skipTutorial;
        
        private TutorialFlag _lastFlag = TutorialFlag.None;
        private bool _isInTutorial;

        [SerializeField] private List<string> tutorialContent;
        [SerializeField] private List<AudioClip> tutorialAudio;
        
        [Header("References")]
        [SerializeField] private FuseBoxController fuseBox;
        [SerializeField] private ScreenInteractController screen;
        [SerializeField] private ButtonKillInteract killButton;
        [SerializeField] private ButtonEnterInteract exitButton;
        [SerializeField] private ButtonNextInteract nextButton;
        [SerializeField] private Animator adonis;
        
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
            
            Debug.Assert(tutorialAudio.Count == tutorialContent.Count, "Length Differ");

            if (_skipTutorial) return;
            StartTutorial();
        }

        public static bool IsInTutorial => Instance._isInTutorial;
        
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
            adonis.gameObject.SetActive(true);
            adonis.Play("Enter");
            TriggerTutorialDialogue(0);
            
            GameManager.Mic.TutorialGlow();
            GameManager.Mic.SetInteractionTo(true);
            // Todo SHOW DEFAULT ANSWER TUTORIAL
            
            // Scene 2
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.AnsweredQuestion);
            TriggerTutorialDialogue(1);
            
            yield return new WaitForSeconds(12f);
            TriggerTutorialDialogue(2);
            
            fuseBox.OnStartEvent();
            
            // Scene 3
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.GeneratorInteracted);
            
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.SatDown);
            TriggerTutorialDialogue(3);

            yield return new WaitForSeconds(12f);
            TriggerTutorialDialogue(4);
            
            // Scene 4
            yield return new WaitForSeconds(12f);
            TriggerTutorialDialogue(5);
            yield return new WaitForSeconds(12f);
            TriggerTutorialDialogue(6);
            yield return new WaitForSeconds(12f);
            TriggerTutorialDialogue(7);
            yield return new WaitForSeconds(5f);
            screen.TutorialGlow();
            screen.SetInteractionTo(true);
            
            // Scene 5
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.ZoomedOutOfHelp);
            TriggerTutorialDialogue(8);
            
            killButton.SetInteractionTo(true);
            exitButton.SetInteractionTo(true);
            
            //Last Scene
            yield return new WaitUntil(() => _lastFlag is TutorialFlag.PressedButtonKill or TutorialFlag.PressedButtonExit);
            TriggerTutorialDialogue(_lastFlag is TutorialFlag.PressedButtonKill ? 9 : 10);
            adonis.Play(_lastFlag is TutorialFlag.PressedButtonKill ? "Drop" : "Exit");
            nextButton.SetInteractionTo(true);
            
            _isInTutorial = false;
        }

        private void TriggerTutorialDialogue(int index)
        {
            UIManager.Dialogue.ShowTutorial(tutorialContent[index]);
            AudioManager.PlayTutorialLine(tutorialAudio[index]);
        }
    }
}
