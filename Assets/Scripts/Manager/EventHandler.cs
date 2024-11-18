using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    [DefaultExecutionOrder(-1)]
    public class EventHandler : MonoBehaviour
    {
        // Publics
        public static EventHandler Instance {get; private set;}
        
        // States
        private readonly List<EventReceiver> _events = new();

        #region SetUp

        public void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Multiple instances of EventHandler");
                Destroy(this);
                return;
            }
            
            Instance = this;
        }

        private void OnDestroy()
        {
            if(Instance == this) Instance = null;
        }

        public void RegisterEventReceiver(EventReceiver eventReceiver) => _events.Add(eventReceiver);
        private void Start() => Debug.Log($"EventHandler has {_events.Count} events registered");
        
        #endregion

        public static void TriggerRandomEvent() => Instance.TriggerEvent(Random.Range(0, Instance._events.Count));
        private void TriggerEvent(int index) => _events[index].Trigger();
    }
}