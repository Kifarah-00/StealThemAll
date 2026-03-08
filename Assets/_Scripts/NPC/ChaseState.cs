using UnityEngine;

public class ChaseState : EnemyState
{
    [SerializeField] float timeToLeaveState = 3f;
    float seeTargetTimer = 99;
    public override void StartState(Enemy _owner)
    {
        base.StartState(_owner);

    }

    public override void StateBehaviour()
    {
        base.StateBehaviour();
        if (CanSeeTarget())
        {
            seeTargetTimer = timeToLeaveState;
            GoToTarget(owner.target);

            if (IsInTargetRange())
            {
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
    }

    public override void EndState()
    {
        base.EndState();
    }

    void AttackTarget()
    {
        Debug.Log("GAME OVER");
    }



}