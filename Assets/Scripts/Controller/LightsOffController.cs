using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Controller
{
    public class LightsOffController: MonoBehaviour
    {
        [SerializeField] private List<Light> lights;
        private float _onValue;
        private Coroutine _lightsRoutine;
        
        private void Awake()
        {
            _onValue = lights[0].intensity;
            FuseBoxController.OnPowerChangeTo += SetLightTo;
        }
        
        private void OnDestroy() => FuseBoxController.OnPowerChangeTo -= SetLightTo;

        private void SetLightTo(bool on)
        {
            if(_lightsRoutine is not null) StopCoroutine(_lightsRoutine);
            _lightsRoutine = StartCoroutine(LerpLights(on ? _onValue : 0f));
        }

        private IEnumerator LerpLights(float targetValue)
        {
            float startValue = lights[0].intensity;
            float elapsedTime = 0;

            while (elapsedTime < LightManager.DefaultLerpTime)
            {
                foreach (var l in lights) l.intensity = Mathf.Lerp(startValue, targetValue, elapsedTime / LightManager.DefaultLerpTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            foreach (var l in lights) l.intensity = targetValue;
        }
    }
}