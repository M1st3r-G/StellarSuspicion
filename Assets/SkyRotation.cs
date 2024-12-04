using System.Collections;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] [Range(0f, 45f)]private float rotationSpeed;

    private void Start() => StartCoroutine(LowRotation());

    private IEnumerator LowRotation()
    {
        while (true)
        {
            transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotationAxis);   
            yield return null;
        }
    }
}
