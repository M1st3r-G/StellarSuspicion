using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class EventReceiver : MonoBehaviour
    {
        [SerializeField] private string customName;
        [SerializeField] [Tooltip("You can Add here what methods should be called if the evnet Triggers")]
        private UnityEvent onEventStart;
        
        private void Awake() => TimeManager.Instance.RegisterEventReceiver(this);
        public void Trigger()
        {
            Debug.LogWarning($"Event {customName} started");
            onEventStart?.Invoke();
        }
    }
}