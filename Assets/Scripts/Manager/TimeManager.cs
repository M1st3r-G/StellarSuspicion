using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{

    public static event System.Action OnDayEnd;
    
    public delegate void OnTimeEventDelegate(System.Action callback);
    public static event OnTimeEventDelegate OnTimeEvent;
    
    [Header("Parameters")]
    [Tooltip("Time of day in Seconds.")][SerializeField] float timeOfDay;
    [Tooltip("Max Events per day")][SerializeField]int maxEventsPerDay;

    private bool eventIsGoingOn;
    private int amountOfEventsToday;
    
    private bool dayTimeisCounting;
    
    
    #region Setup

    public static TimeManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one instance of CameraManager");
            Destroy(gameObject);
            return;
        }
        Debug.Log("Camera Manager");
        _instance = this;
    }


    public void OnDestroy()
    {
        if (_instance == this) _instance = null;
    }
    #endregion

    public void SetDayTimerActive()
    {
        Debug.Log("Timer is active");
        dayTimeisCounting = true;
        StartCoroutine(DayTimer());
    }

    private void FixedUpdate()
    {
        if (!dayTimeisCounting) return;
        if (amountOfEventsToday >= maxEventsPerDay)
        {
            Debug.Log("MaxEventsOverstayed");
            return;
        }

        if (eventIsGoingOn)
        {
            Debug.Log("Event is going on");
            return;
            
        }
        
        if (Random.Range(0f, 1f) < 0.0035f)
        {
            Debug.Log("Event started");
            OnTimeEvent?.Invoke(() => eventIsGoingOn = false);
            eventIsGoingOn = true;
        }
    }
    
    /// <remarks>Maby stop timer while event is happening</remarks>>
    IEnumerator DayTimer()
    {
        yield return new WaitForSeconds(timeOfDay);
        dayTimeisCounting = false;
        OnDayEnd?.Invoke();
        Debug.Log("Day End");
        amountOfEventsToday = 0;
        
    }
    
    
}


