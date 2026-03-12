using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelExit : MonoBehaviour
{
    [Header("Conditions")]
    [SerializeField] private int scoreToExit = 0;

    [SerializeField] GameObject indicator_CanExit, indicator_NoExit;
    // public int scoreToUnlock = Map.scoreToUnlock;

    // [Header("Scene Navigation")]
    // [SerializeField] private string nextLevelName;
    // [SerializeField] private string mainMenuSceneName = "MainMenu";

    // [Header("Interaction distance")]
    // [SerializeField] private float interactRange = 2f;

    // private Transform playerTransform;


    bool playerIsInside = false;

    Controls controls;

    void Awake()
    {
        if (controls == null) controls = new();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Interact.performed += HandleLevelExit;
    }

    void OnDisable()
    {
        controls.Player.Interact.performed -= HandleLevelExit;
        controls.Disable();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            playerIsInside = true;
            int currentScore = ScoreManager.Instance.currentScore;

            if (currentScore < scoreToExit)
            {
                indicator_CanExit.SetActive(false);
                indicator_NoExit.SetActive(true);
            }
            else
            {
                indicator_CanExit.SetActive(true);
                indicator_NoExit.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            playerIsInside = false;

            indicator_CanExit.SetActive(false);
            indicator_NoExit.SetActive(false);

        }
    }


    // void Update()
    // {
    //     if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
    //     {
    //         Debug.Log("TEST");
    //         if (playerIsInside)
    //         {
    //             Debug.Log("PLAYER RANGE");


    //         }

    //     }
    // }

    private void HandleLevelExit(InputAction.CallbackContext ctx)
    {
        // if (ScoreManager.Instance == null)
        // {
        //     SceneManager.LoadScene(mainMenuSceneName);
        //     return;
        // }

        // int finalScore = ScoreManager.Instance.currentScore;

        // if (finalScore >= scoreToUnlock)
        // {
        //     Debug.Log("✅ Level completed! Enough points collected. Loading: " + nextLevelName);
        //     if (finalScore > ScoreManager.HighScore) ScoreManager.HighScore = finalScore;
        //     SceneManager.LoadScene(nextLevelName);
        // }
        // else
        // {
        //     Debug.Log("⛔ Not enough points! Back to main menu...");
        //     if (finalScore > ScoreManager.HighScore) ScoreManager.HighScore = finalScore;
        //     SceneManager.LoadScene(mainMenuSceneName);
        // }
        if (!playerIsInside) return;

        int currentScore = ScoreManager.Instance.currentScore;
        Debug.Log("🚪 E gedrückt! Dein aktueller Score: " + currentScore + " | Benötigt: " + scoreToExit);
        ScoreManager.SetNewHighscore(currentScore);


        if (currentScore < scoreToExit)
        {
            Debug.Log("!!SCORE TOO LOW!!");
        }
        else
        {
            ScoreManager.Instance.ResetScore();
            SceneManager.LoadScene(0);
        }



    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, interactRange);
    // }
}