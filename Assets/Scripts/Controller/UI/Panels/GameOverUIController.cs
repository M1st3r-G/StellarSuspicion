using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller.UI.Panels
{
    public class GameOverUIController : MonoBehaviour
    {
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
            this.gameObject.SetActive(false);
            gameOverPanel.alpha = 0;
        }

        public void GameOver(int score)
        {
            Debug.LogWarning("Game Over");
            this.gameObject.SetActive(true);
            gameOverText.text += score.ToString();
            StartCoroutine(GameOverCoroutine());
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        private IEnumerator GameOverCoroutine()
        {
            float elapsed = 0f;
        
            while (elapsed < fadeoutTime)
            {
                gameOverPanel.alpha = Mathf.Lerp(0, 0.85f, elapsed / fadeoutTime);
                elapsed += Time.deltaTime;
                yield return null;
         
            }
            yield return null;
        }
    
    
    }
}
