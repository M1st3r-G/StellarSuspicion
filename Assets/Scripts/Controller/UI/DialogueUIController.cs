using Data;
using Manager;
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

        private void ShowEnterLine(bool successful)
        {
            textBox.text = successful ? "Vielen Dank" : "Ich werde ihre Kinder essen";
        }

        private void ShowDeathLine(bool successful)
        {
            textBox.text = successful ? "Woher wussten sie es?" : "Aber ich bin unschuldig!";
        }

        public void ShowResolution(AcceptMode acceptMode, bool success)
        {
            if(acceptMode == AcceptMode.Rejected) ShowDeathLine(success);
            else ShowEnterLine(success);
            GameManager.Creature.Clear(acceptMode);
        }
    }
}
