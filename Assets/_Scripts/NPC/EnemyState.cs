using Pathfinding;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float moveSpeed = 4f;
    protected Enemy owner;


    public virtual void StartState(Enemy _owner)
    {
        owner = _owner;
        SetSpeed();
        return;
    }

    public virtual void StateBehaviour()
    {
        return;
    }

    public virtual void EndState()
    {
        return;
    }

    protected bool TargetIsInLineOfSight()
    {
        if (owner.target == null) return false;

        Vector2 direction = (Vector2)owner.target.position - (Vector2)transform.position;
        float distance = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, owner.obstacleLayer);

        if (hit.collider == null)
        {
            Debug.DrawRay(transform.position, direction.normalized * distance, Color.green);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, direction.normalized * hit.distance, Color.red);
            return false;
        }
    }

    public bool CanSeeTarget()
    {
        if (owner.target == null) return false;


        return Vector3.Distance(transform.position, owner.target.position) < owner.detectionRange && TargetIsInLineOfSight();
    }

    protected bool IsInTargetRange()
    {
        if (owner.target == null) return false;


        return Vector3.Distance(transform.position, owner.target.position) < owner.attackRange;
    }

    protected void SetSpeed()
    {
        GetComponent<AIPath>().maxSpeed = moveSpeed;
    }

    protected void GoToTarget(Transform target)
    {
        Seeker seeker = GetComponent<Seeker>();
        if (target != null)
        {
            Debug.Log("starting Path");
            seeker.StartPath(transform.position, target.position);
        }
    }


}