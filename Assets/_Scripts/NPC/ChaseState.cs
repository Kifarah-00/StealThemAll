using Pathfinding;
using UnityEngine;

public class ChaseState : EnemyState
{
    public override void StartState(Enemy _owner)
    {
        base.StartState(_owner);

    }

    public override void StateBehaviour()
    {
        base.StateBehaviour();
        if (CanSeeTarget())
        {
            GoToTarget();

            if (IsInTargetRange())
            {
                AttackTarget();
            }

        }
        else
        {
            owner.ChangeState(owner.IdleState());
        }
    }

    public override void EndState()
    {
        base.EndState();
    }

    void AttackTarget()
    {
        Debug.Log("GAME OVER");
    }

    void GoToTarget()
    {
        Seeker seeker = GetComponent<Seeker>();
        if (owner.target != null)
        {
            seeker.StartPath(transform.position, owner.target.position);
        }
    }

}