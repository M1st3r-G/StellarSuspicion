using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class LightManager : MonoBehaviour
    {
        [SerializeField] List<Light> lights;
        [SerializeField] List<Material> glowingMaterials;
        // Screen Manages itself
        
        private static LightManager _instance;

        private void Awake()
        {
            if (_instance is not null)
            {
                Debug.LogWarning("More than one instance of LightManager");
                Destroy(this);
                return;
            }
            
            _instance = this;
        }

        public static void LightsToState(bool on)
        {
            foreach (Light l in _instance.lights) l.enabled = on;
            foreach (Material m in _instance.glowingMaterials)
            {
                if(on) m.EnableKeyword("_EMISSION");
                else m.DisableKeyword("_EMISSION");
            }
        }
    }
}