using UnityEngine;

public class Attack : MonoBehaviour
{
    #region Variables_Close

    [Header("Attack Parameters")]

    [SerializeField] private float closeCoolDown = 2.5f;
    [SerializeField] private float damage = 1;
    [SerializeField] private float closeRange = 1.8f;
    [SerializeField] private AudioClip CloseSound;

    [Header("Collider Parameters")]

    [SerializeField] private float closeDistance = 0.2f;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Layer Parameters")]
    [SerializeField] private LayerMask playerLayer;

    private float CloseTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;
    private Moving patrool;
    private float StartTimer;
    private float StartScript;

    #endregion

    #region Variables_Large

    [Header("Attack Parameters")]

    [SerializeField] private float largeCoolDown = 2.5f;
    [SerializeField] private float largeRange = 1;
    [SerializeField] private AudioClip SnowSound;

    [Header("Range Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] SnowBalls;

    [Header("Collider Parameters")]

    [SerializeField] private float largeDistance = 0;
    [SerializeField] private BoxCollider2D boxLargeCollider;

    private float LargeTimer = 0;

    #endregion

    #region Variables_Ice

    [Header("Ice Parameters")]

    private float IceDuration = 4.5f;
    private float IceTimer;
    [SerializeField] private AudioClip IceSound;

    [SerializeField] private Transform PlayerPlace;
    [SerializeField] private Transform GroundPlace;
    [SerializeField] private GameObject[] Ices;

    #endregion

    #region Basic_Method

    private void OnEnable()
    {
        StartScript = 1.3f;
        StartTimer = 0;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrool = GetComponentInParent<Moving>();
    }

    private void Update()
    {
        StartTimer += Time.deltaTime;
        if (StartTimer >= StartScript)
        {
            CloseTimer += Time.deltaTime;
            IceTimer += Time.deltaTime;
            LargeTimer += Time.deltaTime;
        }
      
        CloseAttackLogic();

        if (PlayerInSightLarge() && LargeTimer > largeCoolDown && IceTimer > IceDuration)
        {
            System.Random random = new System.Random();//Рандом між різними дальними атаками
            int test = random.Next(0, 100);
            if (test > 50)
            {
                LargeAttackLogic();
            }
            else
            {
                IceAttackLogic();
            }
        }
        else
        {
            LargeAttackLogic();
            IceAttackLogic();
        }

    }

    private void OnDrawGizmos()
    {
        //Close
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + (transform.right * closeRange * transform.localScale.x * closeDistance),
               new Vector3(boxCollider.bounds.size.x * closeRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        //Large
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxLargeCollider.bounds.center + (transform.right * largeRange * transform.localScale.x * largeDistance),
               new Vector3(boxLargeCollider.bounds.size.x * largeRange, boxLargeCollider.bounds.size.y, boxLargeCollider.bounds.size.z));
    }

    #endregion

    #region Custom_Method_Large

    private void LargeAttackLogic()
    {
        if (PlayerInSightLarge())//якщо ігрок в області видимості
        {
            if (LargeTimer >= largeCoolDown)
            {
                AudioManager.instance.PlaySound(SnowSound);
                LargeTimer = 0;
                IceTimer -= 0.8f;
                anim.SetTrigger("snow");
            }
        }

        if (patrool != null)//зупинка при виді ігрока
        {
            patrool.enabled = !PlayerInSightLarge();
        }
    }

    //випускання сніжка
    private void LargeAttack()
    {
        LargeTimer = 0;

        SnowBalls[FindSnowBall()].transform.position = firepoint.position;
        SnowBalls[FindSnowBall()].GetComponent<SnowBall>().ActivateProjectile();
    }

    private int FindSnowBall()
    {
        for (int i = 0; i < SnowBalls.Length; i++)
        {
            if (!SnowBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    //пошук ігрока
    private bool PlayerInSightLarge()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxLargeCollider.bounds.center + (transform.right * largeRange * transform.localScale.x * largeDistance),
            new Vector3(boxLargeCollider.bounds.size.x * largeRange, boxLargeCollider.bounds.size.y, boxLargeCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    #endregion

    #region Custom_Method_Close

    private void CloseAttackLogic()
    {
        if (PlayerInSightClose())//перевірка на ігрока в зоні атаки
        {
            if (CloseTimer >= closeCoolDown)
            {
                AudioManager.instance.PlaySound(CloseSound);
                CloseTimer = 0;
                anim.SetTrigger("vertical");
            }
        }

        if (patrool != null)
        {
            patrool.enabled = !PlayerInSightClose();
        }
    }

    //пошук гравця в зоні атаки
    private bool PlayerInSightClose()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + (transform.right * closeRange * transform.localScale.x * closeDistance),
            new Vector3(boxCollider.bounds.size.x * closeRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void CloseDamage()
    {
        if (PlayerInSightClose())
        {
            playerHealth.TakeDamage(damage);
        }
    }

    #endregion

    #region Method_Ice

    private void IceAttackLogic()
    {
        if (PlayerInSightLarge())
        {
            if (IceTimer >= IceDuration)
            {
                AudioManager.instance.PlaySound(IceSound);
                IceTimer = 0;
                LargeTimer -= 0.8f;
                anim.SetTrigger("ice");
            }
        }

        if (patrool != null)
        {
            patrool.enabled = !PlayerInSightLarge();
        }
    }

    private void IceAttack()
    {
        IceTimer = 0;

        Ices[FindIce()].transform.position = new Vector3(PlayerPlace.position.x, transform.position.y, transform.position.z);
        Ices[FindIce()].GetComponent<Ice>().ActivateProjectile();
    }

    private int FindIce()
    {
        for (int i = 0; i < Ices.Length; i++)
        {
            if (!Ices[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    #endregion
}
