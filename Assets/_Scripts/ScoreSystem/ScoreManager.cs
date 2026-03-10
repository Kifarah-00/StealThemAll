using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

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
    }

    public static void SetNewHighscore(int newScore)
    {
        if (newScore > HighScore)
            HighScore = newScore;
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
