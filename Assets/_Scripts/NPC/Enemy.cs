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
