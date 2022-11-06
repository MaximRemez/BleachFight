using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    #region Variables

    [SerializeField] private float speed = 8;
    [SerializeField] private float resetTime = 3;

    private float lifeTime;

    #endregion

    #region Basic_Method

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
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    //деактивація при закіненні часу
    private void TimeDestroy()
    {
        if (lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion
}
