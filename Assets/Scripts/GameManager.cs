using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public int numMonsters;
    public int numSuccesses;
    
    #region Setup
    public static GameManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one GameManager in scene!");
            Destroy(gameObject);
            return; 
        }
        Debug.Log("GameManager is created!");
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
    
    #endregion
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
