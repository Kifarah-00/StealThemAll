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

    protected bool TargetIsInLineOfSight(Transform _target)
    {
        if (_target == null) return false;

        Vector2 direction = (Vector2)_target.position - (Vector2)transform.position;
        float distance = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, owner.obstacleLayer);

        if (hit.collider == null)
        {
            Debug.DrawRay(transform.position, direction.normalized * distance, Color.green, 10);
            Debug.Log("LOS YES");
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, direction.normalized * hit.distance, Color.red, 10);
            Debug.Log("LOS NO");
            return false;
        }
    }

    public bool CanSeeTarget(Transform _target)
    {
        if (_target == null) return false;

        // Debug.Log("Angle: " + TargetIsInFrontAngle(_target));
        return Vector3.Distance(transform.position, _target.position) < owner.detectionRange && TargetIsInLineOfSight(_target) && TargetIsInFrontAngle(_target);
    }

    protected bool TargetIsInFrontAngle(Transform _target)
    {
       
        Vector2 directionToTarget = (_target.position - transform.position).normalized;

        Vector2 lookDirection = transform.up;

        float angleToTarget = Vector2.Angle(lookDirection, directionToTarget);
        bool canSee = angleToTarget < (owner.viewAngle * 0.5f);

       
        return canSee;
    }

    protected bool IsInTargetRange(Transform _target)
    {
        if (_target == null) return false;


        return Vector3.Distance(transform.position, _target.position) < owner.attackRange;
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
            seeker.StartPath(transform.position, target.position);
        }
    }

}