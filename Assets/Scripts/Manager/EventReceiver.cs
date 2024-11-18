using UnityEngine;

namespace Manager
{
    public class EventReceiver : MonoBehaviour
    {
        [SerializeField] private string customName;
        
        private void Awake() => EventHandler.Instance.RegisterEventReceiver(this);
        public void Trigger() => Debug.Log($"Triggered event {customName}");
    }
}