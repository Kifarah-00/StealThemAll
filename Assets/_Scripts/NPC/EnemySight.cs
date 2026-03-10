using UnityEngine;

public class EnemySight : MonoBehaviour
{

    public float viewAngle = 90f;
    protected bool TargetIsInLineOfSight(Transform _target, Enemy owner)
    {
        if (_target == null) return false;

        Vector2 direction = (Vector2)_target.position - (Vector2)transform.position;
        float distance = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, distance, owner.obstacleLayer);

        if (hit.collider == null)
        {
            Debug.DrawRay(transform.position, direction.normalized * distance, Color.green, 10);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, direction.normalized * hit.distance, Color.red, 10);
            return false;
        }
    }

    public bool CanSeeTarget(Transform _target, Enemy owner)
    {
        if (_target == null) return false;

        // Debug.Log("Angle: " + TargetIsInFrontAngle(_target));
        return Vector3.Distance(transform.position, _target.position) < owner.detectionRange && TargetIsInLineOfSight(_target, owner) && TargetIsInFrontAngle(_target, owner);
    }

    protected bool TargetIsInFrontAngle(Transform _target, Enemy owner)
    {

        Vector2 directionToTarget = (_target.position - transform.position).normalized;

        Vector2 lookDirection = transform.up;

        float angleToTarget = Vector2.Angle(lookDirection, directionToTarget);
        bool canSee = angleToTarget < (viewAngle * 0.5f);


        return canSee;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 lookDir = transform.up;

        Vector3 leftBoundary = Quaternion.Euler(0, 0, viewAngle * 0.5f) * lookDir;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, -viewAngle * 0.5f) * lookDir;

        float detectionRange = 5;

        Gizmos.DrawRay(transform.position, leftBoundary * detectionRange);
        Gizmos.DrawRay(transform.position, rightBoundary * detectionRange);


    }
}
