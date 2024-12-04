using System.Collections.Generic;
using Controller;
using UnityEngine;

namespace Manager
{
    public class LightManager : MonoBehaviour
    {
        [SerializeField] List<Material> glowingMaterials;
        
        private static LightManager _instance;
        
        [SerializeField] [Range(0.1f, 2f)] private float defaultLerptime;
        public static float DefaultLerpTime => _instance.defaultLerptime;

        private void Awake()
        {
            if (_instance is not null)
            {
                Debug.LogWarning("More than one instance of LightManager");
                Destroy(this);
                return;
            }
            
            _instance = this;

            FuseBoxController.OnPowerChangeTo += LightsToState;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
                FuseBoxController.OnPowerChangeTo -= LightsToState;
            }
            
        }

        public static void LightsToState(bool on)
        {
            foreach (Material m in _instance.glowingMaterials)
            {
                if(on) m.EnableKeyword("_EMISSION");
                else m.DisableKeyword("_EMISSION");
            }
        }
    }
}