using System.Collections;
using UnityEngine;

namespace Manager
{
    public class TutorialManager : MonoBehaviour
    {
        private TutorialFlag _lastFlag;
        private bool _isInTutorial;
        
        public static TutorialManager Instance { get; private set; }
        
        public enum TutorialFlag
        {
            None,
            DialogueFinished,
            EnterGreenhouse,
            FocusedSeed,
            PlantedSeed,
            NextDay,
            Watered,
            Replant,
            AddedFertilizer,
            FlowerBlooms,
            DecidedForPlant,
            BookOpened
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

        public void StartTutorial()
        {
            if (_isInTutorial) return;
            
            _isInTutorial = true;
            StartCoroutine(TutorialRoutine());
        }

        public void SetFlag(TutorialFlag type)
        {
            if (_isInTutorial) _lastFlag = type;
        }

        private IEnumerator TutorialRoutine()
        {
            yield return new WaitUntil(() => true);
            _isInTutorial = false;
        }
    }
}
