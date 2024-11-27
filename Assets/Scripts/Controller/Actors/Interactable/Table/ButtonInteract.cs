using Data;
using Manager;
using UnityEngine;

namespace Controller.Actors.Interactable.Table
{
    [RequireComponent(typeof(Animator))]
    public class ButtonInteract : InteractableBase
    {
        private enum ButtonType
        {
            Next, 
            Skip,
            Enter
        }
        
        private Animator _animator;
        private readonly int _stateHash = Animator.StringToHash("Pressed");
        [SerializeField] [Tooltip("The Type of this Button")]
        private ButtonType type;
        
        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        protected override void TriggerInteraction() => _animator.Play(_stateHash);

        public void OnButtonDown()
        {
            // Play Click
            if (type != ButtonType.Next)
            {
                if (GameManager.Creature.CurrentCreature is null)
                {
                    // ErrorSound 
                    return;
                }

                AcceptMode enterMode = type == ButtonType.Enter ? AcceptMode.Allowed : AcceptMode.Rejected;
                GameManager.ResolveCreature(enterMode, GameManager.Creature.CurrentCreature.Value);
                return;
            }

            // Next
            GameManager.Window.SetWindowOpened(true);
            if (GameManager.Creature.ShowingCreature)
            {
                //Error Sound
                return;
            }

            Debug.Log("Let New Creature in");
            GameManager.Creature.SetToCreature(CreatureCreator.GetRandomCreature());
        }
    }
}