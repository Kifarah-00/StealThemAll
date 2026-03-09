using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState = GameState.MainMenu;


    public Action<GameState, GameState> OnChangeState;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeState(GameState newState)
    {
        GameState oldState = currentState;

        currentState = newState;

        OnChangeState?.Invoke(oldState, newState);
    }

    public bool GameIsPaused()
    {
        return currentState == GameState.Paused;
    }

}