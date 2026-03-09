using UnityEngine;

public class Parcel : MonoBehaviour
{
    [SerializeField] int scoreAmount = 10;
    [SerializeField] float weight = 1;
    public float timeToCollect = 1;

    public void CollectParcel()
    {
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();

        if(scoreManager == null) return;

        scoreManager.ChangeScore(scoreAmount);
        this.gameObject.SetActive(false);
    }
}