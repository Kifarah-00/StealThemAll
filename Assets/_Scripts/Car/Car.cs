using Pathfinding;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite horizontal, vertical;

    [SerializeField] Transform pathParent;

    [SerializeField] float arrivedDistance = 0.1f;

    Transform currentTargetPos;

    int currentNode = 0;

    bool GetNewWayPointTrigger = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (GetComponent<AIPath>().remainingDistance < arrivedDistance)
        {
           GetNewWayPointTrigger = true;
        }

        if (GetNewWayPointTrigger)
        {
            GoToNextTarget();
        }
    }

    void GoToNextTarget()
    {
        currentTargetPos = pathParent.GetChild(currentNode);
        currentNode++;
        GetNewWayPointTrigger = false;

        if (currentNode >= pathParent.childCount)
            currentNode = 0;

        if (currentTargetPos == null)
            Debug.LogWarning("KEIN CURRENT TARGET!");

        GoToTarget(currentTargetPos);
    }

    protected void GoToTarget(Transform target)
    {
        Seeker seeker = GetComponent<Seeker>();
        if (target != null)
        {
            seeker.StartPath(transform.position, target.position);
        }
    }

    public void ChangeSprite(CarDirection carDirection, bool flipX)
    {
        if (carDirection == CarDirection.vertical)
        {
            spriteRenderer.sprite = vertical;
        }

        if (carDirection == CarDirection.horizontal)
        {
            spriteRenderer.sprite = horizontal;
        }
        spriteRenderer.flipX = flipX;
    }
}

public enum CarDirection
{
    vertical, horizontal
}
