using Data;
using Manager;
using UnityEngine;

namespace Controller.Actors
{
    public class ClockController : MonoBehaviour
    {
        private int _counter;
    
        public static ClockController Instance;

        [SerializeField] private Vector3 positionForSummondthing;
        [SerializeField]private GameObject firstSprite;
        [SerializeField]private GameObject secondSprite;
        [SerializeField]private GameObject thirdSprite;

    
        #region Setup

        private void Awake()
        {
            if (Instance != null)Destroy(this.gameObject);
            Instance = this;
            _counter = 0;
            firstSprite.SetActive(false);
            secondSprite.SetActive(false);
            thirdSprite.SetActive(false);
            Debug.LogWarning("Clock Controller");
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null; Debug.LogError("Destroyed");
        }

        #endregion

        public void Printout()
        {
            Debug.Log(_counter);
        
            switch (_counter)
            {
                case 0:
                    Debug.LogWarning("Case 0");
                    firstSprite.SetActive(true);
                    break;
                case 1:
                    secondSprite.SetActive(true);
                    break;
                case 2:
                    thirdSprite.SetActive(true);
                    break;
            }
            _counter++;
            AudioManager.PlayEffect(AudioEffect.Print, transform.position);
        }
    
    }
}
