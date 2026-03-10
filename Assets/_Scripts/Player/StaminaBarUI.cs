using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private PlayerStamina playerStamina;
    
    private CanvasGroup canvasGroup;

    void Start()
    {
        if (staminaSlider != null)
        {
            canvasGroup = staminaSlider.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = staminaSlider.gameObject.AddComponent<CanvasGroup>();
            }
        }
    }

    void Update()
    {
        if (playerStamina == null || staminaSlider == null) return;
        
        float staminaPercent = playerStamina.GetStaminaPercent();
        staminaSlider.value = staminaPercent;
        
        if (staminaPercent >= 0.99f)
        {
            canvasGroup.alpha = 0;
        }
        else
        {
            canvasGroup.alpha = 1;
        }
    }
}