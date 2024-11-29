using Controller.Actors.Interactable.Events;
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
        [SerializeField] [Tooltip("The Effects")]
        private AudioSource effects;
    
        private void Awake()
        {
            SetLoopRunning(true);
        }

        private void SetLoopRunning(bool running)
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
            SetLoopRunning(false);
            effects.Play();
            interaction.StartInteraction();
        }

        public void Finished() => SetLoopRunning(true);
    }
}
