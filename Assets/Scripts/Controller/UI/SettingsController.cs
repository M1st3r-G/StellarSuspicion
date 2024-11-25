using Controller.Player;
using Extern;
using Manager;
using UnityEngine;

namespace Controller.UI
{
    public class SettingsController : MonoBehaviour
    {
    
        [SerializeField] [Tooltip("The Standing PlayerController")]
        private PlayerStandController playerStand;
        private CanvasGroup _myGroup;
    
        public void Awake() => _myGroup = GetComponent<CanvasGroup>();

        public void ReturnToPause()
        {
            _myGroup.SetGroupActive(false);
            UIManager.PauseMenu.SetMenuActive(true);
        }

        public void SensitivityChanged(float newValue)
        {
            float newSensitivity = Mathf.Lerp(0.1f,0.5f,newValue);
            playerStand.ChangeSensitivity(newSensitivity);
        }
    
        public void SetMenuActive(bool b) => _myGroup.SetGroupActive(b);
    }
}
