using Pathfinding;
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
        GetComponent<AIPath>().enableRotation = false;
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
            RotateTowardsTarget();
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

        // if (owner != null && owner.target != null)
        //     if (CanSeeTarget(owner.target))
        //         RotateTowardsTarget();
    }

    public override void EndState()
    {
        GetComponent<AIPath>().enableRotation = true;
    }

    void AttackTarget()
    {
        FindFirstObjectByType<EscapeHandler>().StartQTE();
        attackTimer = recoverAfterAttack;

        owner.StopMovement();


    }

    private void RotateTowardsTarget()
    {
        if (owner.target == null) return;

        // RICHTUGN ZUM TARGET
        Vector2 direction = (Vector2)owner.target.position - (Vector2)transform.position;
        direction.Normalize();

        // MATHE
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // LERP ROTATION
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        float rotationSpeed = 360;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * 100f * Time.deltaTime);
    }





}