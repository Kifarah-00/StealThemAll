using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyState currentState = null;
    public EnemyState startingState;
    public LayerMask obstacleLayer;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;

    public Transform target;

    [SerializeField] float timeToUpdate = 0.2f;
    float updateTimer = 99;


    void Start()
    {
        updateTimer = 0;
        if (startingState != null)
        {
            ChangeState(startingState);
        }
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

    [ContextMenu("Set Player as target")]
    public void MarkPlayerAsTarget()
    {
        target = GameObject.FindWithTag("Player").transform;
    }


    //USE THIS FOR PLAYER EVENT
    void OnPlayerSpottedAtIllegalAction()
    {
        if (GetComponent<IdleState>().CanSeeTarget())
        {
            MarkPlayerAsTarget();
        }
    }

    void OnChangeGameState(GameState oldState, GameState newState)
    {
        if (newState == GameState.Paused)
        {
            GetComponent<AIPath>().maxSpeed = 0;
        }

        else if (newState == GameState.InGame)
        {
            GetComponent<AIPath>().maxSpeed = currentState.moveSpeed;
        }
    }
}
