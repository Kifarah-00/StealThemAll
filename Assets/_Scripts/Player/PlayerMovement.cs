using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStamina))]
public class PlayerMovement : MonoBehaviour
{[Header("Speed")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeed = 9f;

    private Rigidbody2D rb;
    private PlayerStamina playerStamina;
    
    private Vector2 moveInput;
    private bool isRunButtonPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        
        playerStamina.UpdateStamina(isRunButtonPressed, isMoving);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        
        bool canRun = isRunButtonPressed && !playerStamina.IsExhausted && isMoving;
        
        float speed = canRun ? runSpeed : moveSpeed;

        speed *= GetComponent<Weight>().GetSpeedMultiplier();

        rb.linearVelocity = moveInput * speed;
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