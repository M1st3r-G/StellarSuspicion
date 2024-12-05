using Data;
using Manager;
using UnityEngine;

namespace Controller.Actors
{
    public class ClockController : MonoBehaviour
    {
        [SerializeField]private PrintoutController firstSprite;
        [SerializeField]private PrintoutController secondSprite;
        [SerializeField]private PrintoutController thirdSprite;

        private int _counter;
    
        #region Setup

        private void Awake()
        {
            firstSprite.gameObject.SetActive(false);
            secondSprite.gameObject.SetActive(false);
            thirdSprite.gameObject.SetActive(false);
        }

        #endregion

        public void Printout()
        {
            AudioManager.PlayEffect(AudioEffect.Print, transform.position);
            switch (_counter)
            {
                case 0:
                    firstSprite.Appear();
                    break;
                case 1:
                    secondSprite.Appear();
                    break;
                case 2:
                    thirdSprite.Appear();
                    break;
            }

            _counter++;
        }
    
    }
}
