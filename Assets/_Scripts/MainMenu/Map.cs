using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public bool isLocked = false;
    public static int scoreToUnlock = 9999;

    public string SCENENAME = "Map_0";

    [SerializeField] GameObject lockedObject;

    public void UnlockMap()
    {
        isLocked = false;
    }

    void Update()
    {
        if (isLocked && lockedObject.activeSelf == false)
        {
            lockedObject.SetActive(true);
            if (ScoreManager.HighScore >= scoreToUnlock)
            {
                UnlockMap();
                lockedObject.SetActive(false);
            }
        }
        
    }

}