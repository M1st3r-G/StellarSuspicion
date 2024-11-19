using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("This is the Transform of the Canvas with the Non See-through Blinder image")]
    [SerializeField] private Transform doorTransform;

    [Header("Parameters")]
    [Tooltip("Theg length to move up inorder to hide the Blinder imae")]
    [Range(0f, 3f)] [SerializeField] private float up;
    [Tooltip("The Time it takes to change the Blinder State")]
    [Range(0f, 3f)][SerializeField] private float closingTime;
    
    // Public
    public bool IsOpen { get; private set; }

    // Temps
    private Coroutine _closingRoutine;

    //private void Awake() => SetWindowOpened(true);

    #region WindowClosing

    public void SetDoorOpend(bool open)
    {
        if(_closingRoutine != null) StopCoroutine(_closingRoutine);
        _closingRoutine = StartCoroutine(ClosingRoutine(open));
    }
        
    private IEnumerator ClosingRoutine(bool open)
    {
        float elapsed = 0f;
        
        Vector3 startPosition = doorTransform.localPosition;
        Vector3 endPosition = open ? up * Vector3.up : Vector3.zero;
        
        IsOpen = open;
        
            
        while (elapsed <= closingTime)
        {
            doorTransform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsed / closingTime);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        doorTransform.localPosition = endPosition;
    }

    #endregion
}
