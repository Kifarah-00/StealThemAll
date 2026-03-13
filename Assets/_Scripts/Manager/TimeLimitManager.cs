using UnityEngine;
using UnityEngine.UI;

public class TimeLimitManager : MonoBehaviour
{
    [SerializeField] float timeLimit = 300;
    [SerializeField] Image fillImage;
    float timer = 999999;

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateUI();
        if (timer <= 0)
        {
            //GAME OVER
            FindFirstObjectByType<EscapeHandler>().GameOverInstant();

        }
    }

    void UpdateUI()
    {
        fillImage.fillAmount = timer / timeLimit;
    }
}
