using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] private Slider staminaSlider;      
    [SerializeField] private PlayerMovement playerMovement; 
    
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
        if (playerMovement == null || staminaSlider == null) return;

        float staminaPercent = playerMovement.GetStaminaPercent();
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