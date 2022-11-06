using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables

    [SerializeField] private float speed = 9;
    private float direction;
    private float lifeGetsuga;
    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator anim;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(12, 13); //Ігнор пустих колізій(зон атак)
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        lifeGetsuga += Time.deltaTime;
        if (lifeGetsuga> 5)
        {
            gameObject.SetActive(false);
        }
    }

    //вмикнення анімації влучення
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explore");

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1.5f);
        }
    }
    #endregion

    #region Custom_Method

    //напрямок атаки
    public void SetDirection(float _direction)
    {
        direction = _direction;
        lifeGetsuga = 0;

        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //деактивація об'єкта
    private void Diactivate()
    {
        gameObject.SetActive(false);
    }
    #endregion

}
