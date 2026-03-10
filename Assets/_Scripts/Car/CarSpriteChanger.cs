using UnityEngine;

public class CarSpriteChanger : MonoBehaviour
{
    [SerializeField] CarDirection carDirection;
    [SerializeField] bool flipX = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Car>(out Car car))
        {
            car.ChangeSprite(carDirection, flipX);
        }
    }
}