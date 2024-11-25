using UnityEngine;

namespace Controller.Actors.Interactable
{
    [RequireComponent(typeof(Animator))]
    public class ButtonInteract : InteractableBase
    {
        private Animator _animator;
        private readonly int _stateHash = Animator.StringToHash("Pressed");
        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        protected override void TriggerInteraction()
        {
            _animator.Play(_stateHash);
        }
    }
}