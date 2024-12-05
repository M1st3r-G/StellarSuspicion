using System.Collections;
using System.Linq;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller.UI.Panels
{
    public class GameOverUIController : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI highscoreText;
        [SerializeField]private TextMeshProUGUI gameOverText;
        [SerializeField]private CanvasGroup gameOverPanel;
        [SerializeField]private float fadeoutTime;
    
        public static GameOverUIController Instance;
        [SerializeField] private TMP_InputField textInput;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one UI Manager in scene.");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            if(Instance==this) Instance = null;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            gameOverPanel.alpha = 0;
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlaymodeManager.Instance.FirstPersonMap.Disable();
            Time.timeScale = 0f;
            
            gameOverText.text = $"Behandelte Aliens: {GameManager.Instance.MonstersAmount}, Genauigkeit: {GameManager.Instance.Accuracy}";
            StartCoroutine(GameOverCoroutine());
        }

        public void AddToLeaderBoard()
        {
            string enteredName = textInput.text;
            if (string.IsNullOrEmpty(enteredName)) enteredName = "Unknown";

            textInput.transform.parent.gameObject.SetActive(false);
            
            LeaderBoardManager.AddToBoard(enteredName, GameManager.Instance.MonstersAmount, GameManager.Instance.Accuracy);
            
            highscoreText.text = LeaderBoardManager.GetTop().Aggregate("",
                (prev, entry) => prev + $"Name: {entry.Name}, Behandelte Aliens: {entry.NumberOfCreatures}, Genauigkeit: {entry.Accuracy}<br>");
        }
        
        public void ReturnToMainMenu() => SceneManager.LoadScene(0);

        private IEnumerator GameOverCoroutine()
        {
            float elapsed = 0f;
        
            while (elapsed < fadeoutTime)
            {
                gameOverPanel.alpha = Mathf.Lerp(0, 0.85f, elapsed / fadeoutTime);
                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }
        }
    }
}
