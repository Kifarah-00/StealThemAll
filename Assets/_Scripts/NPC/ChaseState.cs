using UnityEngine;

public class ChaseState : EnemyState
{
    [SerializeField] float timeToLeaveState = 3f;
    float seeTargetTimer = 99;
    float attackTimer = 80085;
    float recoverAfterAttack = 3;
    public override void StartState(Enemy _owner)
    {
        base.StartState(_owner);
        attackTimer = 0;
    }

    public override void StateBehaviour()
    {
        base.StateBehaviour();
        if (owner.target == null) return;
        if (attackTimer <= 0)
            owner.StartMovement();

        if (CanSeeTarget(owner.target))
        {
            seeTargetTimer = timeToLeaveState;
            GoToTarget(owner.target);

            if (IsInTargetRange(owner.target))
            {
                if (attackTimer <= 0)
                    AttackTarget();
            }

        }
        else
        {

            if (seeTargetTimer < 0)
                owner.ChangeState(owner.startingState);
        }
    }

    void Update()
    {
        seeTargetTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
    }

    public override void EndState()
    {
        base.EndState();
    }

    void AttackTarget()
    {
        FindFirstObjectByType<EscapeHandler>().StartQTE();
        attackTimer = recoverAfterAttack;

        owner.StopMovement();


    }





}