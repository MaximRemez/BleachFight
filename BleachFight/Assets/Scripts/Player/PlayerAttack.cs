using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variables

    [Header("KeyCode")]

    KeyCode Getsuga = KeyCode.Z;
    KeyCode Vertical = KeyCode.X;
    KeyCode Horizontal = KeyCode.C;
    KeyCode Block = KeyCode.V;


    [Header("Getsuga")]

    [SerializeField] private AudioClip GetsugaSound;
    [SerializeField] private float FireCooldown = 4.3f;
    private float LargeCooldownTimer = Mathf.Infinity;

    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] getsugas;
    private Animator anim;
    private PlayerMovement playerMovement;

    [Header("Close")]

    [SerializeField] private AudioClip CloseSound;
    [SerializeField] private float verticalCooldown = 2.7f;
    [SerializeField] private float horizontalCooldown = 1.5f;

    private float verticalCooldownTimer = Mathf.Infinity;
    private float horizontalCooldownTimer = Mathf.Infinity;

    [SerializeField] private GameObject[] vert;
    [SerializeField] private GameObject[] hori;

    [Header("Block")]

    [SerializeField] private AudioClip BlockSound;
    [SerializeField] private float BlockCooldown = 1.5f;
    private float BlockCooldownTimer = Mathf.Infinity;

    [SerializeField] private GameObject[] blocks;

    #endregion

    #region Basic_Method

    //Присваюємо змінним свойства персонажа та скріпт
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    //таймери для можливості атаки і умови для виконання атаки 
    private void Update()
    {
        LargeCooldownTimer += Time.deltaTime;
        verticalCooldownTimer += Time.deltaTime;
        horizontalCooldownTimer += Time.deltaTime;
        BlockCooldownTimer += Time.deltaTime;

        if (Input.GetKey(Block) && BlockCooldownTimer > BlockCooldown && playerMovement.canAttack())
        {
            BlockCooldownTimer = 0;
            AudioManager.instance.PlaySound(BlockSound);
            StartCoroutine(BlockTime());
        }

        if (Input.GetKey(Getsuga) && LargeCooldownTimer > FireCooldown && playerMovement.canAttack())
        {
            anim.SetTrigger("attack");
            LargeCooldownTimer = 0;
            verticalCooldownTimer = 0;
            horizontalCooldownTimer = 0;
        }
        else if (Input.GetKey(Vertical) && verticalCooldownTimer > verticalCooldown && playerMovement.canAttack())
        {
            verticalCooldownTimer = 0;
            horizontalCooldownTimer = 0;
            AudioManager.instance.PlaySound(CloseSound);
            StartCoroutine(VertTime());
        }
        else if (Input.GetKey(Horizontal) && horizontalCooldownTimer > horizontalCooldown && playerMovement.canAttack())
        {
            horizontalCooldownTimer = 0;
            verticalCooldownTimer = 0;
            AudioManager.instance.PlaySound(CloseSound);
            StartCoroutine(HoriTime());
        }

    }

    #endregion

    #region Custom_Method

    //дальня атака(виконується в анімації)
    private void LargeAttack()
    {
        getsugas[FindGetsuga()].transform.position = FirePoint.position;
        getsugas[FindGetsuga()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        AudioManager.instance.PlaySound(GetsugaSound);
        if (Mathf.Sign(transform.localScale.x) == Mathf.Sign(-2))//разворот анімацаії атаки у погляд персонажа
        {
            getsugas[FindGetsuga()].GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            getsugas[FindGetsuga()].GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }

    //пошук снарядів
    private int FindGetsuga()
    {
        for (int i = 0; i < getsugas.Length; i++)
        {
            if (getsugas[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    //затримка нанесення урона при атаці 
    private IEnumerator VertTime()
    {
        anim.SetTrigger("vertical");
        yield return new WaitForSeconds(0.15f);
        vert[0].GetComponent<VertAttack>().SetDirection();
        yield return new WaitForSeconds(0.15f);

        vert[0].GetComponent<VertAttack>().Diactivate();
    }

    //затримка нанесення урона при атаці 
    private IEnumerator HoriTime()
    {
        anim.SetTrigger("horizontal");
        yield return new WaitForSeconds(0.15f);
        hori[0].GetComponent<HoriAttack>().SetDirection();
        yield return new WaitForSeconds(0.15f);

        hori[0].GetComponent<HoriAttack>().Diactivate();
    }

    //затримка ставлення блока 
    private IEnumerator BlockTime()
    {
        anim.SetTrigger("block");
        blocks[0].GetComponent<BlockProj>().SetDirection();
        yield return new WaitForSeconds(0.3f);

        blocks[0].GetComponent<BlockProj>().Diactivate();
    }

    #endregion
}
