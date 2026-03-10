using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class EscapeHandler : MonoBehaviour
{
    [SerializeField] InputActionReference qteAction;
    [SerializeField] float timeToPress = 1f;
    [SerializeField] TMP_Text indicator;
    [SerializeField] UnityEvent onGameOver; 
    [SerializeField] UnityEvent onEscapeSuccess;

    private string expectedControlName;
    private bool qteStarted = false;
    private float timer;

    [ContextMenu("START Escape QTE")]
    public void StartQTE()
    {
        var bindings = qteAction.action.bindings;
        int randomIndex = Random.Range(0, bindings.Count);

        expectedControlName = qteAction.action.GetBindingDisplayString(randomIndex);
        
        qteStarted = true;
        timer = timeToPress;

        indicator.text = $"[{expectedControlName}]";
        indicator.gameObject.SetActive(true);
        
        qteAction.action.Enable();
    }

    void Update()
    {
        if (!qteStarted) return;

        timer -= Time.deltaTime;

        if (qteAction.action.triggered)
        {
            var control = qteAction.action.activeControl;
            if (control != null && control.displayName == expectedControlName)
            {
                Debug.Log("ESCAPE!!");
                onEscapeSuccess?.Invoke();
                StopQTE();
            }
            if (timer <= 0 || control.displayName != expectedControlName) 
            {
                Debug.Log("GameOver");
                onGameOver?.Invoke();
                StopQTE();
            }
        }

        
    }

    void StopQTE()
    {
        qteStarted = false;
        qteAction.action.Disable();
        indicator.text = "";
        indicator.gameObject.SetActive(false);
    }
}