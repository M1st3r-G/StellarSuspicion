using Data;
using TMPro;
using UnityEngine;

namespace Controller.UI
{
    public class DialogueUIController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] [Tooltip("This is the Text Box for Dialogue")]
        private TextMeshProUGUI textBox;
        
        public void ShowInteraction(CreatureAction interaction, CreatureData? currentCreature, bool success = false)
        {
            switch (interaction)
            {
                case CreatureAction.Hello:
                    SetText($"Glorb blorb bla: {currentCreature?.Name}!");
                    break;
                case CreatureAction.Talk:
                case CreatureAction.Exit:
                    SetText(success ? "Vielen Dank" : "Ich werde ihre Kinder essen");
                    break;
                case CreatureAction.Die:
                    SetText(success ? "Woher wussten sie es?" : "Aber ich bin unschuldig!");
                    break;
                default:
                    SetText("Not Set Yet!");
                    break;
            }
        }

        private void SetText(string text) => textBox.text = text;
    }
}
