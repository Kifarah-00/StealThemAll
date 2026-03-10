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
        Weight playerWeight = FindFirstObjectByType<PlayerMovement>().GetComponent<Weight>();
            
        if (playerWeight != null)
        {
            playerWeight.AddWeight(weight);
        }
        this.gameObject.SetActive(false);
    }
}