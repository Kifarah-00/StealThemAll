using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject creditScreen;
    [SerializeField] TMP_Text highScoreText;

    private void Start()
    {
        ShowHighScore();
        
        // Musik starten
        if (AudioManager.instance != null)
        {
            // WICHTIG: Prüfe im Inspector, ob der Name exakt "MenuMusic" ist!
            AudioManager.instance.Play("MenuMusic");
        }
    }

    void ShowHighScore()
    {
        if (highScoreText == null) return;
        highScoreText.text = $"HIGHSCORE: {ScoreManager.HighScore}";
    }

    public void StartGame()
    {
        if (MapSelection.SelectedMap != null)
        {
            // Musik aus
            if (AudioManager.instance != null)
            {
                AudioManager.instance.Stop("MenuMusic"); 
            }

            ResetCurrentScore();
            LoadCorrectMap();
        }
        else
        {
            Debug.LogWarning("No Map Selected!");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleCreditScreen(bool active)
    {
        if (creditScreen == null) return;
        creditScreen.SetActive(active);
    }

    void ResetCurrentScore()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetScore();
    }

    void LoadCorrectMap()
    {
        SceneManager.LoadScene(MapSelection.SelectedMap.SCENENAME);
    }
}