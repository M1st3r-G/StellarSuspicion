using UnityEngine;
using UnityEngine.UI;

namespace Controller.UI
{
    public class InteractUIController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("The mask for the Circle Effect")]
        private Image circle;
        [SerializeField] [Tooltip("The center Pivot")]
        private Image center;
        
        public void SetToAmount(float value) => circle.fillAmount = value;
    }
}