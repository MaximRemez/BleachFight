using UnityEngine;

public class Arrow_trap : MonoBehaviour
{
    #region Variables

    [SerializeField] private float attackCoolDown = 1.3f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float TimerCooldown;

    #endregion

    #region Basic_Method

    //частота атаки
    private void Update()
    {
        TimerCooldown += Time.deltaTime;

        ConditionAttack();
    }

    #endregion

    #region Custom_Method

    //поява стріл
    private void Attack()
    {
        TimerCooldown = 0;

        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    //пошук стріл
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    //умова для атаки
    private void ConditionAttack()
    {
        if (TimerCooldown >= attackCoolDown)
        {
            Attack();
        }
    }

    #endregion

}
