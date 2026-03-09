using UnityEngine;

public class IdleState : EnemyState
{
    public override void StartState(Enemy _owner)
    {
        base.StartState(_owner);
    }

    public override void StateBehaviour()
    {
        base.StateBehaviour();
        if (owner.target != null && CanSeeTarget(owner.target))
        {
            owner.ChangeState(owner.ChaseState());
        }
    }

    public override void EndState()
    {
        base.EndState();
    }

  
}