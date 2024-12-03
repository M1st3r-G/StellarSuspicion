using UnityEngine;

public class DeathAfterTime : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;
    private void Start() => Destroy(gameObject, deathSound.clip.length * 1.2f);
}
