using UnityEngine;

public class IdleState : EnemyState
{
    [SerializeField] float minTimeToTurn = 2, maxTimeToTurn = 10;
    float turnTimer = 99;

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
            //FLIP SIGHT    
            owner.FlipSprite(!GetComponentInChildren<SpriteRenderer>().flipX);

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
            ResetTurnTimer();
            turnTrigger = true;
        }
    }

    void ResetTurnTimer()
    {
        turnTimer = Random.Range(minTimeToTurn, maxTimeToTurn);

    }

}