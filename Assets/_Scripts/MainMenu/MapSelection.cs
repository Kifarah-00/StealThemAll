using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public static Map SelectedMap = null;

    public void SelectMap(Map map)
    {
        if (map.isLocked) return;

        SelectedMap = map;
    }



}