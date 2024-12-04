using UnityEngine;

namespace Controller
{
    public class AdonisController : MonoBehaviour
    {
        private bool _once = false;
    
        public void OnFinishedMovement()
        {
            if (!_once)
            {
                _once = true;
                return;
            }
            gameObject.SetActive(false);
        }
    }
}
