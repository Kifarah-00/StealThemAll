using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI Referenz")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu"; 

    [Header("Audio Settings")]
    // Hier den Namen eintragen, der im AudioManager vergeben wurde (z.B. "GameOverSound")
    [SerializeField] private string gameOverSoundName = "GameOverSound";

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
        
        // --- SOUND ABSPIELEN ---
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play(gameOverSoundName);
        }
        // -----------------------

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
        ScoreManager.Instance.ResetScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        ScoreManager.Instance.ResetScore();
        Application.Quit();
        Debug.Log("Game finished");
    }
}