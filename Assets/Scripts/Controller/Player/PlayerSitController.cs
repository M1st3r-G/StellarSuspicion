using System.Collections;
using Controller.Actors.Interactable;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Player
{
    public class PlayerSitController : PlayerBaseController
    {
        [Header("References")]
        [SerializeField] [Tooltip("The InputAction for Standing Up")]
        private InputActionReference standUpAction;
        [SerializeField] [Tooltip("The Transform of the Chair")]
        private Transform chair;
        
        [Header("Parameters")] 
        [SerializeField] [Tooltip("The DeskInteraction to Disable when Sitting")]
        private DeskController deskInteraction;
        [SerializeField] [Range(0.1f, 1f)] [Tooltip("The Time for the Chair to Rotate")]
        private float rotateTime;

        private Coroutine _rotRoutine;
        
        private void Awake()
        {
            standUpAction.action.performed += OnStandUpAction;
        }

        public override void SetMouseState() => PlaymodeManager.SetMouseTo(true);
        protected override InputActionMap LoadActionMap() => PlaymodeManager.Instance.SittingMap;

        public override void Possess()
        {
            base.Possess();
            if(_rotRoutine is not null) StopCoroutine(_rotRoutine);
            _rotRoutine=StartCoroutine(RotateChair(true));
        }

        private void OnStandUpAction(InputAction.CallbackContext ctx)
        {
            deskInteraction.SetInteractionTo(true);
            PlaymodeManager.StandUp();
            if(_rotRoutine is not null) StopCoroutine(_rotRoutine);
            _rotRoutine=StartCoroutine(RotateChair(false));
        }

        private IEnumerator RotateChair(bool straight)
        {
            Quaternion start = chair.localRotation;
            Quaternion end = Quaternion.Euler(0,straight ? 0 : -60,0);
            
            float elapsedTime = 0;
            while (elapsedTime < rotateTime)
            {
                chair.localRotation = Quaternion.Lerp(start, end, elapsedTime / rotateTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            chair.localRotation = end;
        }
    }
}