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
            LeaderBoardManager.AddToBoard("dName", GameManager.Instance.MonstersAmount,
                GameManager.Instance.Accuracy);
            
            gameObject.SetActive(true);
            gameOverText.text += $"{GameManager.Instance.MonstersAmount} | {GameManager.Instance.Accuracy}";
            highscoreText.text = LeaderBoardManager.GetTop().Aggregate("",
                (prev, entry) => prev + $"{entry.Name}: {entry.NumberOfCreatures} | {entry.Accuracy}<br>");
            StartCoroutine(GameOverCoroutine());
        }

        public void ReturnToMainMenu() => SceneManager.LoadScene(0);

        private IEnumerator GameOverCoroutine()
        {
            float elapsed = 0f;
        
            while (elapsed < fadeoutTime)
            {
                gameOverPanel.alpha = Mathf.Lerp(0, 0.85f, elapsed / fadeoutTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
