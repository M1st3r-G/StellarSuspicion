using Data;
using Manager;
using UnityEngine;

namespace Controller.Actors
{
    public class ClockController : MonoBehaviour
    {
        private int _counter;


        [SerializeField] private Vector3 positionForSummondthing;
        [SerializeField]private GameObject firstSprite;
        [SerializeField]private GameObject secondSprite;
        [SerializeField]private GameObject thirdSprite;

    
        #region Setup

        private void Awake()
        {
            _counter = 0;
            firstSprite.SetActive(false);
            secondSprite.SetActive(false);
            thirdSprite.SetActive(false);
            Debug.LogWarning("Clock Controller");
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
