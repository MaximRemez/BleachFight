using UnityEngine;

public class Lazer : EnemyDamage
{
    #region Variables

    [SerializeField] private float speed = 8;
    [SerializeField] private float resetTime = 3;

    private float lifeTime;
    private Vector3 startPos;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(11, 11, true);
        Physics2D.IgnoreLayerCollision(11, 12, true);
        startPos = transform.position;
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        TimeDestroy();
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }

    #endregion

    #region Custom_Method

    //Активація лазеру
    public void ActivateProjectile()
    {
        GetComponentInParent<LazerHolder>().ChangeAngle();
        transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    //Деактивація по закінченню часу
    private void TimeDestroy()
    {
        if (lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

}
