using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public static int SelectedMap = 99;

    public bool isBlocked = true;

    public void SelectMap(int index)
    {
        if (isBlocked)
            SelectedMap = index;
    }

    
    public void UnlockMap()
    {
        isBlocked = false;
    }
}