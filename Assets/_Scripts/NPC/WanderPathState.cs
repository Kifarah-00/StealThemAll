using Pathfinding;
using UnityEngine;

public class WanderPathState : EnemyState
{
    [SerializeField] Transform pathParent;
    Transform currentTargetPos;

    int currentNode = 0;

    public override void StartState(Enemy _owner)
    {
        base.StartState(_owner);

        GoToNextTarget();

    }

    public override void StateBehaviour()
    {
        base.StateBehaviour();
        if (CanSeeTarget())
        {
            owner.ChangeState(owner.ChaseState());
        }

        float arrivedDistance = 0.2f;

        if (GetComponent<AIPath>().remainingDistance < arrivedDistance) 
        {
            GoToNextTarget();
        }

    }

    public override void EndState()
    {
        base.EndState();
    }

    void GoToNextTarget()
    {
        Debug.Log("Go to target " + currentNode);
        currentTargetPos = pathParent.GetChild(currentNode);
        currentNode++;

        if (currentNode >= pathParent.childCount)
            currentNode = 0;

        if (currentTargetPos == null)
            Debug.LogWarning("KEIN CURRENT TARGET!");

        GoToTarget(currentTargetPos);
    }


}