using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI; 

public class EscapeHandler : MonoBehaviour
{
    [Header("Referenzen")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] InputActionReference qteAction;
    [SerializeField] float timeToPress = 1.5f;

    [Header("UI (Nur Slider & Taste)")]
    [SerializeField] TMP_Text indicator;     
    [SerializeField] Slider timerSlider;    

    [Header("Events")]
    [SerializeField] UnityEvent onGameOver; 
    [SerializeField] UnityEvent onEscapeSuccess;

    private string expectedControlName;
    private bool qteStarted = false;
    private float timer;

    [ContextMenu("START Escape QTE")]
    public void StartQTE()
    {
        if (playerMovement != null) 
            playerMovement.SetMovementEnabled(false);
        
        var bindings = qteAction.action.bindings;
        int randomIndex = Random.Range(0, bindings.Count);
        expectedControlName = qteAction.action.GetBindingDisplayString(randomIndex);
        
        qteStarted = true;
        timer = timeToPress;
        
        indicator.text = $"[{expectedControlName}]";
        indicator.gameObject.SetActive(true);

        if (timerSlider != null) 
        {
            timerSlider.gameObject.SetActive(true);
            timerSlider.maxValue = timeToPress;
            timerSlider.value = timeToPress;
        }
        
        qteAction.action.Enable();
    }

    void Update()
    {
        if (!qteStarted) return;
        
        timer -= Time.deltaTime;
        
        if (timerSlider != null)
        {
            timerSlider.value = timer;
        }
        
        if (timer <= 0)
        {
            Debug.Log("Zu langsam!");
            onGameOver?.Invoke();
            StopQTE(false);
            return;
        }

        // Prüfung: Taste gedrückt?
        if (qteAction.action.triggered)
        {
            var control = qteAction.action.activeControl;
            
            if (control != null && control.displayName == expectedControlName)
            {
                Debug.Log("Erfolg!");
                onEscapeSuccess?.Invoke();
                StopQTE(true);
            }
            else
            {
                Debug.Log("Falsche Taste!");
                onGameOver?.Invoke();
                StopQTE(false);
            }
        }
    }

    void StopQTE(bool allowMovementAgain)
    {
        qteStarted = false;
        qteAction.action.Disable();
        
        indicator.gameObject.SetActive(false);
        if (timerSlider != null) 
        {
            timerSlider.gameObject.SetActive(false);
        }
        
        if (allowMovementAgain && playerMovement != null)
        {
            playerMovement.SetMovementEnabled(true);
        }
    }
}