using UnityEngine;

public class Weight : MonoBehaviour
{[Header("Gewichts-Einstellungen")]
    [SerializeField] private float currentWeight = 0f;[SerializeField] private float maxWeight = 100f; // Ab hier ist der Spieler extrem langsam[Header("Bewegungs-Einfluss")][Tooltip("Wie viel % der Geschwindigkeit bleibt bei vollem Gewicht übrig? (0.3 = 30% Speed)")]
    [Range(0f, 1f)] 
    [SerializeField] private float minSpeedMultiplier = 0.3f;
    
    public void AddWeight(float amount)
    {
        currentWeight += amount;
        Debug.Log("Gewicht aufgesammelt! Aktuelles Gewicht: " + currentWeight + " / " + maxWeight);
    }
    
    public void RemoveWeight(float amount)
    {
        currentWeight -= amount;
        if (currentWeight < 0) currentWeight = 0;
    }
    
    public float GetSpeedMultiplier()
    {
        float weightRatio = currentWeight / maxWeight;
        
        weightRatio = Mathf.Clamp01(weightRatio);
        
        return Mathf.Lerp(1f, minSpeedMultiplier, weightRatio);
    }

    public float GetCurrentWeight()
    {
        return currentWeight;
    }
}