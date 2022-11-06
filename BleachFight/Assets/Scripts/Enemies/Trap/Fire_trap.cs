using UnityEngine;
using System.Collections;

public class Fire_trap : MonoBehaviour
{
    #region Variables

    [SerializeField] private float damage = 1;

    [Header("FireTrap Timers")]

    [SerializeField] private float activationDelay = 0.4f;
    [SerializeField] private float activeTime = 2;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool WasTriggered;
    private bool IsActive;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    //Якщо ігрок стоїть в огні
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!WasTriggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (IsActive)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    #endregion

    #region Custom_Method

    //затримка активації
    private IEnumerator ActivateFireTrap()
    {
        WasTriggered = true;
        spriteRend.color = Color.red;
        yield return new WaitForSeconds(activationDelay);

        spriteRend.color = Color.white;
        IsActive = true;
        anim.SetBool("Active", true);
        yield return new WaitForSeconds(activeTime);


        WasTriggered = false;
        IsActive = false;
        anim.SetBool("Active", false);
    }

    #endregion
}