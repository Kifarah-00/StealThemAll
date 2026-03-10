using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI Referenz")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu"; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void RestartGame()
    {
        ScoreManager.Instance.ResetScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game finished");
    }
}