using UnityEngine;

public class Ice : MonoBehaviour
{
    #region Variables

    [SerializeField] private Health PlayerHP;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D boxCollider;

    private float lifeTime;
    private float resetTime = 1f;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        TimeDestroy();
    }

    #endregion

    #region Custom_Method

    public void ActivateProjectile()
    {
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    //деактивація після закінчення часу
    private void TimeDestroy()
    {
        if (lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        //Close
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center,
               new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    //пошук ворога в місті атаки
    private bool PlayerInSightClose()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            PlayerHP = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void IceDamage()
    {
        boxCollider.enabled =true;
        if (PlayerInSightClose())
        {
            PlayerHP.TakeDamage(1);
        }
    }
    #endregion

}
