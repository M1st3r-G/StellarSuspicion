using System;
using System.Collections;
using UnityEngine;

namespace Controller.Actors.Interactable.Table.Buttons
{
    public abstract class ButtonBaseInteract : InteractableBase
    {
        private Coroutine _buttonRoutine;
        [SerializeField] [Range(0.5f, 4f)] [Tooltip("The time for the button to lower")]
        private float baseDownTime = 1f;

        protected override void TriggerInteraction()
        {
            if(_buttonRoutine is not null) StopCoroutine(_buttonRoutine);
            _buttonRoutine = StartCoroutine(MoveToState(true, baseDownTime, OnButtonDown));
        }

        public override void SetInteractionTo(bool pEnabled)
        {
            base.SetInteractionTo(pEnabled);
            
            if(_buttonRoutine is not null) StopCoroutine(_buttonRoutine);
            _buttonRoutine = StartCoroutine(MoveToState(!pEnabled, 0.5f, null));
        }

        protected abstract void OnButtonDown();

        private IEnumerator MoveToState(bool down, float downTime, Action onFinished)
        {
            Vector3 startposition = transform.localPosition;
            Vector3 targetPosition = down ?  new Vector3(0.0305f, -0.0275f, 0f) : Vector3.zero;

            float elapsedTime = 0f;

            while (elapsedTime < downTime)
            {
                transform.localPosition = Vector3.Lerp(startposition, targetPosition, elapsedTime / downTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = targetPosition;
            
            onFinished?.Invoke();
        }
    }
}