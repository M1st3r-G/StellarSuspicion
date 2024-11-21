using Extern;
using Manager;
using UnityEngine;

namespace Controller.UI
{
    public class SettingsController : MonoBehaviour
    {
    
        [SerializeField] private PlayerController player;

        private CanvasGroup _myGroup;
    
        public static SettingsController Instance;
    
        #region Setup
    
        public void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Multiple instances of SettingsController found!");
                Destroy(this);
                return;
            }
            
            Instance = this;
        
            _myGroup = GetComponent<CanvasGroup>();
        }

        private void OnDestroy()
        {
            if(Instance == this) Instance = null;
        }

        #endregion

        public void ReturnToPause()
        {
            _myGroup.SetGroupActive(false);
            UIManager.PauseMenu.SetMenuActive(true);
        }

        public void SensitivityChanged(float newValue)
        {
            float newSensitivity = Mathf.Lerp(0.1f,0.5f,newValue);
            player.ChangeSensitivity(newSensitivity);
        }
    
        public void SetMenuActive(bool b) => _myGroup.SetGroupActive(b);
    }
}
