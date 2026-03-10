using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string MAPNAME_0 = "map_0", MAPNAME_1 = "map_1", MAPNAME_2 = "map_2";
    [SerializeField] GameObject creditScreen;
    [SerializeField] TMP_Text highScoreText;


    private void Start()
    {
        ShowHighScore();
    }

    void ShowHighScore()
    {
        if (highScoreText == null) return;
        highScoreText.text = $"HIGHSCORE: {ScoreManager.HighScore}";
    }

    public void StartGame()
    {
        Debug.Log($"STARTING: {MapSelection.SelectedMap}");
        if (MapSelection.SelectedMap != null)
        {
            LoadCorrectMap();
        }
        else
        {
            Debug.LogWarning("No Map Selected!");
        }
        // ResetCurrentScore();
        // LoadCorrectMap();

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
        ScoreManager.Instance.ResetScore();
    }

    void LoadCorrectMap()
    {
        SceneManager.LoadScene(MapSelection.SelectedMap.SCENENAME);
    }
}