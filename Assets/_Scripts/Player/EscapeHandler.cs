using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EscapeHandler : MonoBehaviour
{
    public static EscapeHandler instance;

    [Header("Referenzen")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] InputActionReference qteAction;
    [SerializeField] float timeToPress = 1.5f;

    [Header("UI")]
    [SerializeField] TMP_Text indicator;
    [SerializeField] Slider timerSlider;

    private string expectedControlName;
    private bool qteStarted = false;
    private float timer;

    Enemy attackingEnemy = null;

    [ContextMenu("START Escape QTE")]
    public void StartQTE(Enemy attacker)
    {
        if (attacker != null) attackingEnemy = attacker;

        if (qteStarted)
        {
            timer -= 1;
            return;
        }
        if (playerMovement != null) playerMovement.SetMovementEnabled(false);

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
        if (timerSlider != null) timerSlider.value = timer;

        if (timer <= 0)
        {
            GameOver();
            return;
        }

        if (qteAction.action.triggered)
        {
            var control = qteAction.action.activeControl;
            if (control != null && control.displayName == expectedControlName)
            {
                StopQTE(true);
            }
            else
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        StartCoroutine(TriggerGameOver());
    }

    public void GameOverInstant()
    {
        StartCoroutine(TriggerGameOver());
        FindFirstObjectByType<PlayerMovement>().PlayCaughtAnimation();
        StopQTE(false);
        if (GameOverManager.Instance != null)
        {
            GameOverManager.Instance.ShowGameOverScreen();
        }
    }


    IEnumerator TriggerGameOver()
    {
        if (attackingEnemy != null) attackingEnemy.PlayAttackAnimation();
        FindFirstObjectByType<PlayerMovement>().PlayCaughtAnimation();
        StopQTE(false);

        yield return new WaitForSecondsRealtime(2);

        if (GameOverManager.Instance != null)
        {
            GameOverManager.Instance.ShowGameOverScreen();
        }
    }

    public void StopQTE(bool success)
    {
        qteStarted = false;
        qteAction.action.Disable();
        indicator.gameObject.SetActive(false);
        if (timerSlider != null) timerSlider.gameObject.SetActive(false);

        if (success && playerMovement != null)
        {
            if (attackingEnemy != null)
            {

                attackingEnemy.PlayHurtAnimation();
                attackingEnemy = null;
            }
            playerMovement.SetMovementEnabled(true);
        }
    }
}