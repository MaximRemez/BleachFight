using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
   
    [SerializeField] private float runSpeed = 8;
    [SerializeField] private float jumpPower = 14;
    private float wallJumpCooldown;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    #endregion

    #region Basic_Method

    //Якщо персонаж знаходиться на платформі то рухається разом з нею
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
        {
           transform.parent = collision.transform;
        }
    }

    //Якщо покинув платформу то перестає рухатись разом
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
        {
            transform.parent = null;
        }
    }

    //Присваюємо змінним свойства персонажа
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    //Виконання по оновленню кадра
    private void Update()
    {
        Movement();
        DropFromMap();

        ChangeDirection();
        ChangeAnimation();  
    }

    #endregion

    #region Custom_Method

    //Смерть при випаданні з карти
    private void DropFromMap()
    {
        if (transform.position.y < -50)
        {
            GetComponent<Health>().TakeDamage(5);
        }
    }

    //зміна взгляду персонажа при повороті
    private void ChangeDirection()
    {
        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (Input.GetAxis("Horizontal") < -0.01f)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }

    }

    //змінна анімації на біг
    private void ChangeAnimation()
    {
        anim.SetBool("run", Input.GetAxis("Horizontal") != 0);
        anim.SetBool("grounded", isGrounded());
    }

    #region Movement

    private void Movement()
    {
        if (wallJumpCooldown > 0.2f)// якщо не тільки шо стрибнув на стіні
        {
            Move();

            if (onWall() && !isGrounded())// щоб не падав зі стін
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else// повернення фізики падіння
            {
                body.gravityScale = 3;
            }

            Jump();

        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
      
    }

    // рух при натисканні стрелок
    private void Move()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, body.velocity.y);
    }


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())// якзо на землі
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                anim.SetTrigger("jump");
            }
            else if (onWall() && !isGrounded())// якщо не на землі
            {
                if (Input.GetAxis("Horizontal") == 0)// якщо не натискаються стрелки то відліпає від стіни
                {
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 0);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * 3, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                }

                wallJumpCooldown = 0;
            }
  
        }
    }

    #endregion

    #region Checkers

    //перевірка чи персонаж на землі
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //перевірка чи персонаж на стіні
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.2f, wallLayer);
        return raycastHit.collider != null;
    }

    //умови для атаки
    public bool canAttack()
    {
        return Input.GetAxis("Horizontal") == 0 && isGrounded() && !onWall();
    }
    #endregion

    #endregion

}
