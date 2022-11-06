using UnityEngine;

public class SnowBall : EnemyDamage
{
    #region Variables

    [SerializeField] private float speed = 8.5f;
    [SerializeField] private float resetTime = 3;

    private float lifeTime;
    private Vector3 startPos;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(11, 11, true);
        Physics2D.IgnoreLayerCollision(11, 12, true);
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        TimeDestroy();
    }

    //деактивація при попаданні
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }

    #endregion

    #region Custom_Method

    //активація
    public void ActivateProjectile()
    {
        GetComponentInParent<SnowBallHold>().ChangeAngle();
        transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    private void TimeDestroy()
    {
        if (lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

}
