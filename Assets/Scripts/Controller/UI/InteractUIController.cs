using UnityEngine;
using UnityEngine.UI;

namespace Controller.UI
{
    public class InteractUIController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The mask for the Circle Effect")]
        private Image circle;

        public void SetToAmount(float value) => circle.fillAmount = value;
    }
}