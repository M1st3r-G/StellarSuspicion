using Controller.Player;
using Manager;
using UnityEngine;

namespace Controller.UI.Panels
{
    public class SettingsController : UIPanel
    {
    
        [Header("References")]
        [SerializeField] [Tooltip("The Standing PlayerController")]
        private PlayerStandController playerStand;
    
        public void ReturnToPause()
        {
            SetMenuActive(false);
            UIManager.PauseMenu.SetMenuActive(true);
        }

        public void SensitivityChanged(float newValue)
        {
            float newSensitivity = Mathf.Lerp(0.1f,0.5f,newValue);
            playerStand.ChangeSensitivity(newSensitivity);
        }
    }
}
