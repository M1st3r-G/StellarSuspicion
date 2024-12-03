using System;
using Controller.Actors.Interactable.Events;
using Data;
using Manager;
using UnityEngine;

namespace Controller
{
    public class FuseBoxController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The Interaction of this System")]
        private FuseInteraction interaction;

        [Header("Parameters")] 
        [SerializeField] [Tooltip("The Humming Loop")]
        private AudioSource loop1;
        [SerializeField] [Tooltip("The Machine Loop")]
        private AudioSource loop2;

        public static event Action<bool> OnPowerChangeTo;
        
        private void Awake() => TriggerPowerEvent(true);

        private void TriggerPowerEvent(bool running)
        {
            OnPowerChangeTo?.Invoke(running);
            SetAudioLoopTo(running);
        }

        private void SetAudioLoopTo(bool running)
        {
            if (running)
            {
                loop1.Play();
                loop2.Play();
            }
            else
            {
                loop1.Stop();
                loop2.Stop();
            }
        }

        public void OnStartEvent()
        {
            TriggerPowerEvent(false);
            AudioManager.PlayEffect(AudioEffect.PowerOff, transform.position);
            interaction.StartInteraction();
        }

        public void Finished() => TriggerPowerEvent(true);
    }
}
