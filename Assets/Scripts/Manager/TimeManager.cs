using System.Collections;
using UnityEngine;

namespace Manager
{
    public class TimeManager : MonoBehaviour
    {
        public static event System.Action OnDayEnd;
        public delegate void OnTimeEventDelegate(System.Action callback);
        public static event OnTimeEventDelegate OnTimeEvent;
    
        [Header("Parameters")]
        [Tooltip("Time of day in Seconds.")]
        [SerializeField] private float timeOfDay;
        [Tooltip("Max Events per day")]
        [SerializeField] private int maxEventsPerDay;

        private bool _eventIsGoingOn;
        private int _amountOfEventsToday;
    
        private bool _dayTimeIsCounting;
    
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
        }
    
        public void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    
        #endregion

        public void SetDayTimerActive()
        {
            Debug.Log("Timer is active");
            _dayTimeIsCounting = true;
            StartCoroutine(DayTimer());
        }

        private void FixedUpdate()
        {
            if (!_dayTimeIsCounting) return;
            if (_amountOfEventsToday >= maxEventsPerDay)
            {
                Debug.Log("MaxEventsOverstayed");
                return;
            }

            if (_eventIsGoingOn)
            {
                Debug.Log("Event is going on");
                return;
            }
            
            if (!(Random.Range(0f, 1f) < 0.0035f)) return;

            Debug.Log("Event started");
            OnTimeEvent?.Invoke(() => _eventIsGoingOn = false);
            _eventIsGoingOn = true;
        }
    
        /// <remarks>Maybe stop timer while event is happening</remarks>>
        private IEnumerator DayTimer()
        {
            yield return new WaitForSeconds(timeOfDay);
            _dayTimeIsCounting = false;
            OnDayEnd?.Invoke();
            Debug.Log("Day End");
            _amountOfEventsToday = 0;
        
        }
    
    
    }
}


