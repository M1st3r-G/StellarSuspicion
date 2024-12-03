using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUIController : MonoBehaviour
{
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void GameOver(int score)
    {
        this.gameObject.SetActive(true);
        GetComponentInChildren<TextMeshPro>().text = score.ToString();
    }
}
