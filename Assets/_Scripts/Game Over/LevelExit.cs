using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelExit : MonoBehaviour
{
    [Header("Conditions")]
    [SerializeField] private int scoreToExit = 0;
    public int scoreToUnlock = Map.scoreToUnlock;

    [Header("Scene Navigation")]
    [SerializeField] private string nextLevelName;     
    [SerializeField] private string mainMenuSceneName = "MainMenu"; 

    [Header("Interaction distance")]
    [SerializeField] private float interactRange = 2f; 
    [SerializeField] private GameObject exitZone; 

    private Transform playerTransform;
    private bool isExiting = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null || isExiting) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);
        bool isPlayerInRange = distance <= interactRange;

        if (exitZone != null)
        {
            exitZone.SetActive(isPlayerInRange);
        }

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (isPlayerInRange)
            {
                if (ScoreManager.Instance != null)
                {
                    int currentScore = ScoreManager.Instance.currentScore;
                    Debug.Log("🚪 E gedrückt! Dein aktueller Score: " + currentScore + " | Benötigt: " + scoreToExit);
                }
                else
                {
                    Debug.LogWarning("⚠️ Kein ScoreManager gefunden!");
                }
                // ------------------------------------------

                isExiting = true;
                HandleLevelExit();
            }
            else
            {
                Debug.Log("❌ E gedrückt, aber du bist zu weit weg! (Distanz: " + distance + ")");
            }
        }
    }

    private void HandleLevelExit()
    {
        if (ScoreManager.Instance == null)
        {
            SceneManager.LoadScene(mainMenuSceneName);
            return;
        }

        int finalScore = ScoreManager.Instance.currentScore;
        
        if (finalScore >= scoreToUnlock)
        {
            Debug.Log("✅ Level completed! Enough points collected. Loading: " + nextLevelName);
            if (finalScore > ScoreManager.HighScore) ScoreManager.HighScore = finalScore;
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.Log("⛔ Not enough points! Back to main menu...");
            if (finalScore > ScoreManager.HighScore) ScoreManager.HighScore = finalScore;
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}