using Pathfinding;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    EnemyState currentState = null;
    public EnemyState startingState;
    public LayerMask obstacleLayer;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;

    public Transform target;

    [SerializeField] float timeToUpdate = 0.2f;
    [SerializeField] EnemySight sight;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator anim;
    
    private AIPath aiPath;
    float updateTimer = 99;

    void Awake()
    {
        aiPath = GetComponent<AIPath>();
    }

    void Start()
    {
        updateTimer = 0;
        if (startingState != null)
        {
            ChangeState(startingState);
        }
        if (FindFirstObjectByType<ParcelInteractionTrigger>())
            FindFirstObjectByType<ParcelInteractionTrigger>().onParcelPicked += OnPlayerPickUpParcel;
    }

    void OnDisable()
    {
        if (FindFirstObjectByType<ParcelInteractionTrigger>())
            FindFirstObjectByType<ParcelInteractionTrigger>().onParcelPicked -= OnPlayerPickUpParcel;
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null) currentState.EndState();
        currentState = newState;
        currentState.StartState(this);
    }

    void Update()
    {
        // Animationen jeden Frame aktualisieren für maximale Flüssigkeit
        UpdateAnimationParameters();

        updateTimer -= Time.deltaTime;
        if (updateTimer < 0)
        {
            if (currentState != null)
            {
                currentState.StateBehaviour();
            }

            FlipSprite();
            updateTimer = timeToUpdate;
        }
    }

    void UpdateAnimationParameters()
    {
        if (anim == null) return;

        // Geschwindigkeit für den Animator berechnen
        float currentSpeed = aiPath.velocity.magnitude;
        
        // Setze einen Float für "Speed" (gut für Übergänge)
        anim.SetFloat("Speed", currentSpeed);
        
        // Setze den Bool "IsMoving", falls du diesen beibehalten willst
        anim.SetBool("IsMoving", currentSpeed > 0.1f);
    }

    // Diese Methode kann von deinen EnemyStates (z.B. AttackState) aufgerufen werden
    public void PlayAttackAnimation()
    {
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }
    }

    // Falls der Gegner Schaden nimmt
    public void PlayHurtAnimation()
    {
        if (anim != null)
        {
            anim.SetTrigger("Hurt");
        }
    }
    
    public EnemyState IdleState()
    {
        return GetComponent<IdleState>();
    }

    public EnemyState ChaseState()
    {
        return GetComponent<ChaseState>();
    }

    void FlipSprite()
    {
        Vector2 direction = aiPath.desiredVelocity;
        
        if (direction.x < -0.1f)
        {
            spriteRenderer.flipX = true;
            FlipSight(rightSide: false);
        }
        else if (direction.x > 0.1f)
        {
            spriteRenderer.flipX = false;
            FlipSight(rightSide: true);
        }
    }

    // Überladene Methode für manuelle Flips aus States heraus
    public void FlipSprite(bool rightSide)
    {
        spriteRenderer.flipX = !rightSide; // Da flipX true links bedeutet, wenn das Sprite nach rechts schaut
        FlipSight(rightSide);
    }

    void FlipSight(bool rightSide)
    {
        if (sight == null) return;
        sight.transform.up = rightSide ? Vector2.right : Vector2.left;
    }

    public void StopMovement()
    {
        aiPath.maxSpeed = 0;
        // Sofort Speed auf 0 setzen für Animation
        anim.SetFloat("Speed", 0);
    }

    public void StartMovement()
    {
        if(currentState != null)
            aiPath.maxSpeed = currentState.moveSpeed;
    }

    public bool CanSeeTarget(Transform _target)
    {
        return sight.CanSeeTarget(_target, this);
    }

    void MarkPlayerAsTarget()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player != null) target = player.transform;
    }
    
    [ContextMenu("PLAYER PICK UP SIMULATION")]
    public void OnPlayerPickUpParcel()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && sight.CanSeeTarget(player.transform, this))
        {
            MarkPlayerAsTarget();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.coral;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}