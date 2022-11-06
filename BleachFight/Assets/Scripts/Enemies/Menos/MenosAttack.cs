using UnityEngine;

public class MenosAttack : MonoBehaviour
{
    #region Variables_Close

    [SerializeField] private ScoreManager score;

    [Header("Attack Parameters")]

    [SerializeField] private float closeCoolDown = 2.5f;
    [SerializeField] private float damage = 1;
    [SerializeField] private float closeRange = 1.8f;
    [SerializeField] private AudioClip LazerSound;
    [SerializeField] private AudioClip MenosCloseSound;

    [Header("Collider Parameters")]

    [SerializeField] private float closeDistance = 0.2f;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Layer Parameters")]
    [SerializeField] private LayerMask playerLayer;

    private float closeTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;
    private Patrool patrool;

    #endregion

    #region Variables_Large

    [Header("Attack Parameters")]

    [SerializeField] private float largeCoolDown = 2.5f;
    [SerializeField] private float largeRange = 1;

    [Header("Range Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] lazers;

    [Header("Collider Parameters")]

    [SerializeField] private float largeDistance = 0;
    [SerializeField] private BoxCollider2D boxLargeCollider;

    private float LargeTimer = Mathf.Infinity;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrool = GetComponentInParent<Patrool>();
    }

    private void Update()
    {
        closeTimer += Time.deltaTime;
        LargeTimer += Time.deltaTime;

        CloseAttackLogic();
        LargeAttackLogic();
    }

    //Рисунок області(для редактора)
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
        if (PlayerInSightLarge())//якщо ігрок в зоні дії
        {
            if (LargeTimer >= largeCoolDown)
            {
                LargeTimer = 0;
                if(LazerSound != null)
                {
                    AudioManager.instance.PlaySound(LazerSound);
                }
                anim.SetTrigger("large");
            }
        }

        if (patrool != null)//зупинка якщо ігрок в зоні дії
        {
            patrool.enabled = !PlayerInSightLarge();
        }
    }

    //випуск лазера
    private void LargeAttack()
    {
        LargeTimer = 0;

        lazers[FindLazer()].transform.position = firepoint.position;
        lazers[FindLazer()].GetComponent<Lazer>().ActivateProjectile();
    }

    //знаходження лазеру
    private int FindLazer()
    {
        for (int i = 0; i < lazers.Length; i++)
        {
            if (!lazers[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    //перевірка на знаходження ігрока в зоні атаки
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
        if (PlayerInSightClose())//якщо ігрок в зоні атаки
        {
            if (closeTimer >= closeCoolDown)
            {
                closeTimer = 0;
                if (MenosCloseSound != null)
                {
                    AudioManager.instance.PlaySound(MenosCloseSound);
                }
             
                anim.SetTrigger("close");
            }
        }

        if (patrool != null)//зупинка
        {
            patrool.enabled = !PlayerInSightClose();
        }
    }

    private bool PlayerInSightClose()// пошук гравця
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

    //нанесення урону
    private void CloseDamage()
    {
        if (PlayerInSightClose())
        {
            playerHealth.TakeDamage(damage);
        }
    }
    
    #endregion

    //Кількість вбитих ворогів
    private void CounterKill()
    {
        score.GetComponent<ScoreManager>().Kill();
    }
}
