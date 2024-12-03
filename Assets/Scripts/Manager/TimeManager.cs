using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    [DefaultExecutionOrder(-5)]
    public class TimeManager : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] [Tooltip("Time of day in Seconds.")]
        private float timeOfDay;
        
        [FormerlySerializedAs("_maxEvents")] [SerializeField] [Tooltip("The Amount of events this day.")]
        private int maxEvents;

        
        // States
        private int _eventsLeft;
        private float _bounds;
    
        private readonly List<EventReceiver> _eventReceiver = new();
        
        // Public
        public static event System.Action OnDayEnd;
        
        #region Setup

        public static TimeManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of CameraManager");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            float deviation = -Mathf.Exp(-(maxEvents / 4f)) + 1f;
            _bounds = deviation * timeOfDay / (2 * maxEvents);
        }
    
        private void Start() => Debug.Log($"EventHandler has {_eventReceiver.Count} events registered");
        
        public void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    
        #endregion

        #region TimerManagemt

        public void StartTimerActive()
        {
            Debug.Log("Timer is active");
            
            _eventsLeft = maxEvents;
            StartCoroutine(DayTimer());
        }
    
        private IEnumerator DayTimer()
        {
            float endTime = Time.time + timeOfDay;
            
            while (_eventsLeft > 0)
            {
                //Wait till Event
                float nextWaitTime = CalcNextWaitTime();
                yield return new WaitForSeconds(nextWaitTime);
                
                // Trigger Event
                Debug.Log($"Event {maxEvents - _eventsLeft} started");
                TriggerRandomEvent();
                _eventsLeft -= 1;
            }
            
            Debug.Log("Max Events triggered");
            
            // When Day Over
            yield return new WaitForSeconds(endTime - Time.time);
            
            Debug.Log("Day End");
            OnDayEnd?.Invoke();
        }

        private float CalcNextWaitTime() =>
            timeOfDay / maxEvents + 2 * _bounds * (Random.Range(0f, 1f) - Random.Range(0f, 1f));

        #endregion
        
        #region EventHandling

        public void RegisterEventReceiver(EventReceiver eventReceiver) => _eventReceiver.Add(eventReceiver);

        public static void TriggerRandomEvent() 
            => Instance.TriggerEvent(Random.Range(0, Instance._eventReceiver.Count));
        
        private void TriggerEvent(int index) => _eventReceiver[index].Trigger();

        #endregion
    }
}