using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    Controls controls;

    void Awake()
    {
        controls = new();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.PauseGame.performed += OnPressPause;
    }

    void OnDisable()
    {
        controls.Disable();
        controls.Player.PauseGame.performed -= OnPressPause;
    }

    void OnPressPause(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.currentState == GameState.MainMenu) return;

        if (GameManager.Instance.GameIsPaused())
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            GameManager.Instance.ChangeState(GameState.InGame);
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            GameManager.Instance.ChangeState(GameState.Paused);
        }
    }
}