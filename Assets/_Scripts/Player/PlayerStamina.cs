using UnityEngine;

public class PlayerStamina : MonoBehaviour
{[Header("Stamina settings")]
    [SerializeField] float maxStamina = 100f;
    [SerializeField] float staminaDrain = 30f;
    [SerializeField] float staminaRegen = 20f;
    [SerializeField] float minStaminaToRun = 20f;

    private float currentStamina;
    
    public bool IsExhausted { get; private set; }

    void Start()
    {
        currentStamina = maxStamina; 
    }
    
    public void UpdateStamina(bool isTryingToRun, bool isMoving)
    {
        bool isActuallyRunning = isTryingToRun && isMoving && !IsExhausted;

        if (isActuallyRunning)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                IsExhausted = true; 
            }
        }
        else
        {
            currentStamina += staminaRegen * Time.deltaTime;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
            
            if (IsExhausted && currentStamina >= minStaminaToRun)
            {
                IsExhausted = false;
            }
        }
    }

    public float GetStaminaPercent()
    {
        return currentStamina / maxStamina;
    }
}
