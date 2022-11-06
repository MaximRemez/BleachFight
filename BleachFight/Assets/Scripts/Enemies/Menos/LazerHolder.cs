using UnityEngine;

public class LazerHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform player;
    private float angleRad;
    private float angle;

    private void Update()
    {
        ChangeDirection();
    }

    //Розрахунок кута між гравцем та ворогом
    public void ChangeAngle()
    {
        angleRad = Mathf.Atan2(Mathf.Abs((player.position.y - 0.8f) - enemy.position.y), Mathf.Abs((player.position.x) - enemy.position.x));
        angle = (angleRad * 180.00f) / Mathf.PI;

        if (transform.localScale.x > 0)
        {
            if(player.position.y > enemy.position.y)
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

    //розворот в потрібному напрямку
    private void ChangeDirection()
    {
        transform.localScale = enemy.localScale;
    }
}
