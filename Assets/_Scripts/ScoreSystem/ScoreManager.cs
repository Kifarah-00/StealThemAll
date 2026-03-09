using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    
    [SerializeField] TMP_Text scoreText;
    public int currentScore = 0;

    public static int HighScore = 0;

    void Start()
    {
        UpdateUI();
    }

    public void ChangeScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}";
    }
}
