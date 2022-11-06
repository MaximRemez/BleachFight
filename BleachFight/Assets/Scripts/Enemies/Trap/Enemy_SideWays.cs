using UnityEngine;

public class Enemy_SideWays : MonoBehaviour
{
    #region Variables

    [SerializeField] private float moveDistance = 6;
    [SerializeField] private float speed = 10;
    private bool movingLeft;
    private float rightEdge;
    private float leftEdge;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        leftEdge = transform.position.x - moveDistance;
        rightEdge = transform.position.x + moveDistance;
    }

    private void Update()
    {
        Move();
    }

    #endregion

    #region Custom_Method

    private void Move()
    {
        if (movingLeft)
        {
            if(transform.position.x > leftEdge)//Рух до границі
            {
                transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)//Рух до границі
            {
                transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    #endregion

}
