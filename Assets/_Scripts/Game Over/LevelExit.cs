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
        if (!playerIsInside) return;

        int currentScore = ScoreManager.Instance.currentScore;
        ScoreManager.SetNewHighscore(currentScore);


        if (currentScore < scoreToExit)
        {
            Debug.Log("!!SCORE TOO LOW!!");
        }
        else
        {
            AudioManager.instance.Play("GameWon");
            ScoreManager.Instance.ResetScore();
            SceneManager.LoadScene(0);
        }



    }

}