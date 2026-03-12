using UnityEngine;

public class TutorialInfoIngame : MonoBehaviour
{
    
    void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }
}
