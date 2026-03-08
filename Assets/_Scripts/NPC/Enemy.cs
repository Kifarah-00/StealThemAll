using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float idleMoveSpeed = 4f;
    EnemyState currentState = null;
    [SerializeField] EnemyState startingState;
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

}
