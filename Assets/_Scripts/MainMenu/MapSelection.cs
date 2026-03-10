using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    public static Map SelectedMap = null;
    [SerializeField] Button playButton;

    public void SelectMap(Map map)
    {
        if (map.isLocked) return;

        SelectedMap = map;
        playButton.interactable = true;
    }



}