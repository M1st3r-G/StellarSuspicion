using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class TimeManager : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] [Tooltip("Time of day in Seconds.")]
        private float timeOfDay;
        
        // States
        private int _eventsLeft;
        private int _maxEvents;
        private float _deviation;
    
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

            _maxEvents = Mathf.RoundToInt(timeOfDay / 2.5f);
            _deviation = -1.25f * Mathf.Exp(-(_maxEvents / 4f)) + 1.25f;
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
            
            _eventsLeft = _maxEvents;
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
                Debug.LogWarning($"Event {_maxEvents - _eventsLeft} started");
                TriggerRandomEvent();
                _eventsLeft -= 1;
            }
            
            Debug.LogWarning("Max Events triggered");
            
            // When Day Over
            yield return new WaitUntil(()=> Time.time < endTime);
            Debug.Log("Day End");
            OnDayEnd?.Invoke();
        }

        private float CalcNextWaitTime() => 1.25f + 2 * _deviation * (Random.Range(0f, 1f) - Random.Range(0f, 1f));

        #endregion
        
        #region EventHandling

        public void RegisterEventReceiver(EventReceiver eventReceiver) => _eventReceiver.Add(eventReceiver);

        public static void TriggerRandomEvent() 
            => Instance.TriggerEvent(Random.Range(0, Instance._eventReceiver.Count));
        
        private void TriggerEvent(int index) => _eventReceiver[index].Trigger();

        #endregion
    }
}