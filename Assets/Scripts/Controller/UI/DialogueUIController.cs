using TMPro;
using UnityEngine;

namespace Controller.UI
{
    public class DialogueUIController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("This is the Text Box for Dialogue")]
        private TextMeshProUGUI textBox;
        
        public void SetText(string text) => textBox.text = text;
    }
}
