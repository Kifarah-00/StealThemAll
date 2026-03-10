using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Conditions")]
    [SerializeField] private int scoreToExit = 0; 

    [Header("Szene Navigation")]
    [SerializeField] private string nextLevelName;     
    [SerializeField] private string mainMenuSceneName = "MainMenu"; 
    [SerializeField] private GameObject exitZone;

    private bool isExiting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isExiting)
        {
            isExiting = true;
            HandleLevelExit();
        }
    }

    private void HandleLevelExit()
    {
        if (ScoreManager.Instance != null)
        {
            int finalScore = ScoreManager.Instance.currentScore;

            if (finalScore >= scoreToExit)
            {
                if (finalScore > ScoreManager.HighScore)
                {
                    ScoreManager.HighScore = finalScore;
                }
                
                Debug.Log("Highscore is: " + ScoreManager.HighScore);
            }
            else
            {
                if (finalScore > ScoreManager.HighScore) ScoreManager.HighScore = finalScore;
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
    }
}