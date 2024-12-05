using System.Collections;
using UnityEngine;

namespace Controller.Actors
{
    public class PrintoutController : MonoBehaviour
    {
        [SerializeField] private ClockController clock;
        private Vector3 _endPosition;
        private Vector3 _startPosition;

        private void Awake()
        {
            _endPosition = transform.position;
            _startPosition = clock.transform.position;
            transform.position = _startPosition;
        }

        public void Appear()
        {
            gameObject.SetActive(true);
            StartCoroutine(AppearRoutine());
        }

        private IEnumerator AppearRoutine()
        {
            yield return new WaitForSeconds(1f);
            float elapsed = 0f;

            while (elapsed < 3f)
            {
                transform.position = Vector3.Lerp(_startPosition, _endPosition, elapsed / 3f);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = _endPosition;
        }
    }
}