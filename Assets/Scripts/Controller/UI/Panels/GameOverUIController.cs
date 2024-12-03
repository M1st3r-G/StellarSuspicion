using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI gameOverText;
    
    public static GameOverUIController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one UI Manager in scene.");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void OnDestroy()
    {
       if(instance==this) instance = null;
    }


    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void GameOver(int score)
    {
        this.gameObject.SetActive(true);
        gameOverText.text = score.ToString();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
