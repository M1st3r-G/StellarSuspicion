using UnityEngine;

namespace Controller
{
    public class DeathAfterTime : MonoBehaviour
    {
        [SerializeField] private AudioSource deathSound;
        private void Start()
        {
            if(deathSound?.clip is null) Destroy(gameObject);
            else Destroy(gameObject, deathSound.clip.length * 1.2f);
        }
    }
}
