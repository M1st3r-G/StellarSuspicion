using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{

    public static event System.Action OnDayEnd;
    public static event System.Action OnTimeEvent;
    
    [Header("Parameters")]
    [Tooltip("Time of day in Seconds.")][SerializeField] float timeOfDay;

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
               
    }

    IEnumerator DayTimer()
    {
        dayTimeisCounting = true;
        yield return new WaitForSeconds(timeOfDay);
        dayTimeisCounting = false;
    }
    
    
}


