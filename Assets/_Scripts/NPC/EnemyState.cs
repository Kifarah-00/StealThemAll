using Unity.VisualScripting;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    Enemy owner;


    public virtual void StartState(Enemy _owner)
    {
        owner = _owner;
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
}