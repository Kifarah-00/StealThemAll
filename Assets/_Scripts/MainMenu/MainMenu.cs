using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] MapSelection mapSelection;
    [SerializeField] string MAPNAME_0 = "map_0", MAPNAME_1 = "map_1", MAPNAME_2 = "map_2";

    public void StartGame()
    {
        ResetCurrentScore();
        LoadCorrectMap();

    }

    public void ExitGame()
    {

    }

    public void ShowCredits()
    {

    }

    void ResetCurrentScore()
    {

    }

    void LoadCorrectMap()
    {
        switch (MapSelection.SelectedMap)
        {
            case 0:
                SceneManager.LoadScene(MAPNAME_0);
                break;

            case 1:
                SceneManager.LoadScene(MAPNAME_0);
                break;

            case 2:
                SceneManager.LoadScene(MAPNAME_0);
                break;
        }
    }
}