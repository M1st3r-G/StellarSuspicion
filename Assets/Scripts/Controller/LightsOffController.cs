using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class LightsOffController: MonoBehaviour
    {
        [SerializeField] private List<Light> lights;
        
        private void Awake() => FuseBoxController.OnPowerChangeTo += SetLightTo;

        private void SetLightTo(bool on)
        {
            foreach (var l in lights) l.enabled = on;
        }
    }
}