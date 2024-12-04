
using System.Linq.Expressions;
using Data;
using Manager;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    private int counter;
    
    public static ClockController instance;

    [SerializeField] private Vector3 positionForSummondthing;
    [SerializeField]private GameObject firstSprite;
    [SerializeField]private GameObject secondSprite;
    [SerializeField]private GameObject thirdSprite;

    
    #region Setup

    private void Awake()
    {
        if (instance != null)Destroy(this.gameObject);
        instance = this;
        counter = 0;
        firstSprite.SetActive(false);
        secondSprite.SetActive(false);
        thirdSprite.SetActive(false);
        Debug.LogWarning("Clock Controller");
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null; Debug.LogError("Destroyed");
    }

    #endregion

    public void Printout()
    {
        Debug.Log(counter);
        
        switch (counter)
        {
            case 0:
                Debug.LogWarning("Case 0");
                firstSprite.SetActive(true);
                break;
            case 1:
                secondSprite.SetActive(true);
                break;
            case 2:
                thirdSprite.SetActive(true);
                break;
        }
        counter++;
        AudioManager.PlayEffect(AudioEffect.Print, transform.position);
    }
    
}
