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
        attackTimer = 0;
    }

    public override void StateBehaviour()
    {
        base.StateBehaviour();
        if (owner.target == null) return;

        if (attackTimer <= 0)
            owner.StartMovement();

        if (owner.CanSeeTarget(owner.target))
        {
            seeTargetTimer = timeToLeaveState;
            GoToTarget(owner.target);

            if (IsInTargetRange(owner.target))
            {
                if (owner.target.TryGetComponent<PlayerMovement>(out PlayerMovement player))
                {
                    if (player.isDead) return;
                }
                
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


    protected bool IsInTargetRange(Transform _target)
    {
        if (_target == null) return false;


        return Vector3.Distance(transform.position, _target.position) < owner.attackRange;
    }

    public override void EndState()
    {
        transform.right = Vector2.right;
    }

    void AttackTarget()
    {
        owner.PlayAttackAnimation();
        FindFirstObjectByType<EscapeHandler>().StartQTE(this.owner);
        attackTimer = recoverAfterAttack;

        owner.StopMovement();

    }

}