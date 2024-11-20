using System.Collections;
using System.Collections.Generic;
using Controller;
using Unity.VisualScripting;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    
    [SerializeField]PlayerController player;
    #region Setup
    
    public static SettingsController instance;
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of SettingsController found!");
            Destroy(this);
            return;
        }
            
        instance = this;
    }

    private void OnDestroy()
    {
        if(instance == this) instance = null;
    }


    #endregion

    public static void ReturnToNormalMenu()
    {
        Debug.Log("Return to Normal Menu");
    }

    public void SensetivityChanged(float newValue)
    {
        float newSensetivity = Mathf.Lerp(0.1f,0.5f,newValue);
        player.ChangeSensitivity(newSensetivity);
    }
}
