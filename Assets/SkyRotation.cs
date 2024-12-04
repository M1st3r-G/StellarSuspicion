using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LowRotation());
    }

    private IEnumerator LowRotation()
    {
        while (true)
        {
            transform.rotation *= new Quaternion();   
            yield return null;
        }
    }
}
