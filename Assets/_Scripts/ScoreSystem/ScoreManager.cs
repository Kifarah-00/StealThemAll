using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] TMP_Text highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] TMP_Text scoreText;
    public int currentScore = 0;

    public static int HighScore = 0;

    void Start()
    {
        UpdateUI();
        SceneManager.sceneLoaded += ShowHighScore;
    }

    void OnDisable()
    {
         SceneManager.sceneLoaded -= ShowHighScore;
    }


    public static void SetNewHighscore(int newScore)
    {
        if (newScore > HighScore)
            HighScore = newScore;
    }

    void ShowHighScore(Scene scene, LoadSceneMode mode)
    {
        if (highScoreText == null) return;
        highScoreText.text = $"HIGHSCORE: {ScoreManager.HighScore}";
    }


    public void ChangeScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}";
    }
}
