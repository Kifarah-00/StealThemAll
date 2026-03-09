using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Geschwindigkeit")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeed = 9f;

    [Header("Ausdauer Einstellungen")]
    [SerializeField] float maxStamina = 100f;
    [SerializeField] float staminaDrain = 30f;
    [SerializeField] float staminaRegen = 20f;
    [SerializeField] float minStaminaToRun = 20f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    
    private float currentStamina;
    private bool isRunButtonPressed;
    private bool isExhausted;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina; 
    }

    void Update()
    {
        HandleStamina();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleStamina()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        
        bool isActuallyRunning = isRunButtonPressed && isMoving && !isExhausted;

        if (isActuallyRunning)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isExhausted = true; 
            }
        }
        else
        {
            currentStamina += staminaRegen * Time.deltaTime;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
            
            if (isExhausted && currentStamina >= minStaminaToRun)
            {
                isExhausted = false;
            }
        }
    }

    void MovePlayer()
    {
        bool canRun = isRunButtonPressed && !isExhausted && moveInput.sqrMagnitude > 0.01f;
        float speed = canRun ? runSpeed : moveSpeed;

        rb.linearVelocity = moveInput * speed;
    }
    
    public float GetStaminaPercent()
    {
        return currentStamina / maxStamina;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Run(InputAction.CallbackContext context)
    {
        isRunButtonPressed = context.ReadValueAsButton();
    }
}