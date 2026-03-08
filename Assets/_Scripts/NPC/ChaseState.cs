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
    }

    public override void EndState()
    {
        base.EndState();
    }

    void AttackTarget()
    {

    }

    void GoToTarget()
    {

    }

    bool CanSeeTarget()
    {
        return false;
    }

    bool IsInTargetRange()
    {
        return false;
    }
}