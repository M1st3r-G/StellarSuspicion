using UnityEngine;

public class AdonisController : MonoBehaviour
{
    public void OnFinishedMovement() => gameObject.SetActive(false);
}
