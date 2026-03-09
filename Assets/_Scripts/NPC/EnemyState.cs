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