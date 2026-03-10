using UnityEngine;

public class IdleState : EnemyState
{
    [SerializeField] float minTimeToTurn = 2, maxTimeToTurn = 10;
    float turnTimer = 99;

    Vector2[] possibleLookDirections = { Vector2.left, Vector2.down, Vector2.right, Vector2.up };
    Vector2 currentLookDirection = Vector2.up;

    bool turnTrigger = false;

    public override void StartState(Enemy _owner)
    {
        base.StartState(_owner);
        ResetTurnTimer();
    }

    public override void StateBehaviour()
    {
        base.StateBehaviour();
        if (turnTrigger)
        {
            transform.right = currentLookDirection;
            turnTrigger = false;
        }

        if (owner.target != null && owner.CanSeeTarget(owner.target))
        {
            owner.ChangeState(owner.ChaseState());
        }
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        turnTimer -= Time.deltaTime;

        if (turnTimer <= 0)
        {
            currentLookDirection = possibleLookDirections[Random.Range(0, possibleLookDirections.Length)]; // TURN IN RANDOM DIRECTIon
            ResetTurnTimer();
            turnTrigger = true;
        }
    }

    void ResetTurnTimer()
    {
        turnTimer = Random.Range(minTimeToTurn, maxTimeToTurn);

    }

}