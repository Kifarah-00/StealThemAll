using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStamina))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer render;

    [Header("Speed")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeed = 9f;

    [Header("Audio")]
    [SerializeField] public string runSoundName  = "RunSound";
    private bool isCurrentlyRunning = false;

    private Rigidbody2D rb;
    private PlayerStamina playerStamina;

    private Vector2 moveInput;
    private bool isRunButtonPressed;
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    public void SetMovementEnabled(bool state)
    {
        canMove = state;
        if (!state)
        {
            rb.linearVelocity = Vector2.zero;
            moveInput = Vector2.zero;
        }
    }

    void Update()
    {
        if (!canMove) return;

        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        playerStamina.UpdateStamina(isRunButtonPressed, isMoving);
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        MovePlayer();
        HandleRunAudio(); // Audio-Zustandsprüfung
        anim.SetFloat("Speed", Math.Abs(rb.linearVelocity.magnitude));
    }

    void MovePlayer()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        bool canRun = isRunButtonPressed && !playerStamina.IsExhausted && isMoving;
        float speed = canRun ? runSpeed : moveSpeed;

        var weight = GetComponent<Weight>();
        if (weight != null) speed *= weight.GetSpeedMultiplier();

        rb.linearVelocity = moveInput * speed;

        FlipRender();
    }

    public void HandleRunAudio()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        bool canRun = isRunButtonPressed && !playerStamina.IsExhausted && isMoving;

        // Sound starten, wenn der Renn-Zustand beginnt
        if (canRun && !isCurrentlyRunning)
        {
            isCurrentlyRunning = true;
            if (AudioManager.instance != null) AudioManager.instance.Play(runSoundName);
        }
        // Sound stoppen, wenn wir aufhören zu rennen
        else if (!canRun && isCurrentlyRunning)
        {
            isCurrentlyRunning = false;
            if (AudioManager.instance != null) AudioManager.instance.Stop(runSoundName);
        }
    }

    public void PlayCaughtAnimation()
    {
        anim.SetTrigger("PlayerCaught");
    }

    void FlipRender()
    {
        if (rb.linearVelocity.x < 0)
        {
            render.flipX = true;
        }
        else if (rb.linearVelocity.x > 0)
        {
            render.flipX = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!canMove) return;
        moveInput = context.ReadValue<Vector2>();
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (!canMove) return;
        isRunButtonPressed = context.ReadValueAsButton();
    }
}

