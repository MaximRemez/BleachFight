using UnityEngine;

public class SnowBallHold : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform player;
    private float angleRad;
    private float angle;

    private void Update()
    {
        ChangeDirection();
    }

    //Пошук кута між босом та гравцем
    public void ChangeAngle()
    {
        angleRad = Mathf.Atan2(Mathf.Abs((player.position.y) - enemy.position.y), Mathf.Abs((player.position.x) - enemy.position.x));
        angle = (angleRad * 180.00f) / Mathf.PI;

        if (transform.localScale.x > 0)
        {
            if (player.position.y > enemy.position.y)
            {
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -angle);
            }
        }
        else
        {
            if (player.position.y > enemy.position.y)
            {
                transform.rotation = Quaternion.Euler(0, 0, -angle);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

    }

    //змінна напрямку
    private void ChangeDirection()
    {
        transform.localScale = enemy.localScale;
    }
}
