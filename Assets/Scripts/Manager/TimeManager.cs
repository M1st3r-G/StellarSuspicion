using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private int _maxEventsInRuntime;
        
        // States
        private int _eventsLeft;
        private float _bounds;
    
        private readonly List<EventReceiver> _eventReceiver = new();
        [SerializeField] [Range(0f, 2f)]private float waitTimeInMins = 1f;

        // Public
        
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
            _maxEventsInRuntime = maxEvents;
        }
    
        private void Start()
        {
            Debug.Log($"EventHandler has {_eventReceiver.Count} events registered");
            Invoke(nameof(StartNewDay), 60f * waitTimeInMins);
        }

        public void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    
        #endregion

        #region TimerManagemt

        private void StartNewDay() => StartCoroutine(DayTimer(_maxEventsInRuntime));

        private IEnumerator DayTimer(int internalMax)
        {
            float endTime = Time.time + timeOfDay;
            
            _eventsLeft = internalMax;
            
            while (_eventsLeft > 0)
            {
                //Wait till Event
                float nextWaitTime = CalcNextWaitTime();
                yield return new WaitForSeconds(nextWaitTime);
                
                // Trigger Event
                Debug.Log($"Event {maxEvents - _eventsLeft} started");
                TriggerRandomEvent();
                _eventsLeft--;
            }
            
            Debug.Log("Max Events triggered");
            
            // When Day Over
            yield return new WaitForSeconds(endTime - Time.time);
            
            Debug.Log("Day End");
            _maxEventsInRuntime++;
            StartNewDay();
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