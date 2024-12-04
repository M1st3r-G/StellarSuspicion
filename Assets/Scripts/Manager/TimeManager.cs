using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    [DefaultExecutionOrder(-5)]
    public class TimeManager : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] [Tooltip("Time of day in Seconds.")]
        private float timeOfDay = 150;
        
        [SerializeField] [Tooltip("The Amount of events this day.")]
        private int maxEvents = 3;

        private int _maxEventsInRuntime;
        
        // States
        private int _eventsLeft;
        private float _bounds;

        [SerializeField] private List<EventReceiver> eventReceiver;
        [SerializeField] [Range(0f, 2f)] private float waitTimeInMins = 1f;

        // Public
        
        #region Setup

        private static TimeManager _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("More than one instance of CameraManager");
                Destroy(gameObject);
                return;
            }

            _instance = this;

            float deviation = -Mathf.Exp(-(maxEvents / 4f)) + 1f;
            _bounds = deviation * timeOfDay / (2 * maxEvents);
            _maxEventsInRuntime = maxEvents;
        }
    
        private void Start() => Debug.Log($"EventHandler has {eventReceiver.Count} events registered");

        public void OnDestroy()
        {
            if (_instance == this) _instance = null;
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

        public static void TriggerRandomEvent() 
            => _instance.TriggerEvent(GetRandomIndexForEvent());

        // 0: Fuse, 1: Trapdoor, 2: Window 
        private static int GetRandomIndexForEvent() =>
            Random.Range(0f, 1f) switch
            {
                < 0.4f => 0,
                < 0.8f => 1,
                _ => 2
            };

        private void TriggerEvent(int index) => eventReceiver[index].Trigger();

        #endregion

        public static void StartEvents() => _instance.InnerStartEvents();
        private void InnerStartEvents() => Invoke(nameof(StartNewDay), 60f * waitTimeInMins);
    }
}