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
    float updateTimer = 99;



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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.coral;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

    }

    public EnemyState IdleState()
    {
        return GetComponent<IdleState>();
    }

    public EnemyState ChaseState()
    {
        return GetComponent<ChaseState>();
    }


    void MarkPlayerAsTarget()
    {
        AudioManager.instance.Play("NPCAlarm");
        target = GameObject.FindWithTag("Player").transform;
    }


    //USE THIS FOR PLAYER EVENT
    [ContextMenu("PLAYER PICK UP SIMULATIOn")]
    public void OnPlayerPickUpParcel()
    {
        // Debug.Log("CHECKING ANGLE FOR:  " + GameObject.FindWithTag("Player").transform + " - " + currentState.CanSeeTarget(GameObject.FindWithTag("Player").transform));
        if (sight.CanSeeTarget(GameObject.FindWithTag("Player").transform, this))
        {
            MarkPlayerAsTarget();
        }
    }

    // void OnChangeGameState(GameState oldState, GameState newState)
    // {
    //     if (newState == GameState.Paused)
    //     {
    //         StopMovement();
    //     }

    //     else if (newState == GameState.InGame)
    //     {
    //         StartMovement();
    //     }
    // }

    void FlipSprite()
    {
        Vector2 direction = GetComponent<AIPath>().desiredVelocity;

        // Debug.Log($"DIRECTION: {direction} ");
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
            FlipSight(rightSide: false);
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
            FlipSight(rightSide: true);
        }

        if (direction != Vector2.zero)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    public void FlipSprite(bool rightSide)
    {

        if (rightSide)
        {
            spriteRenderer.flipX = true;
            FlipSight(rightSide: false);
        }
        else if (!rightSide)
        {
            spriteRenderer.flipX = false;
            FlipSight(rightSide: true);
        }
    }

    void FlipSight(bool rightSide)
    {
        if (rightSide)
        {

            GetComponentInChildren<EnemySight>().transform.up = Vector2.right;
        }
        else
        {
            GetComponentInChildren<EnemySight>().transform.up = Vector2.left;
        }
    }
    public void StopMovement()
    {
        GetComponent<AIPath>().maxSpeed = 0;
    }

    public void StartMovement()
    {
        GetComponent<AIPath>().maxSpeed = currentState.moveSpeed;
    }

    public bool CanSeeTarget(Transform _target)
    {
        return sight.CanSeeTarget(_target, this);
    }


}
