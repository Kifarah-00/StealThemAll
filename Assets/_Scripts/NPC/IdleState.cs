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
        if (CanSeeTarget())
        {
            owner.ChangeState(owner.ChaseState());
        }
    }

    public override void EndState()
    {
        base.EndState();
    }

  
}