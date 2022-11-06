using UnityEngine;

public class Garganta : MonoBehaviour
{
    #region Variables

    [SerializeField] private float attackCoolDown = 9;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] menoses;
    private float TimerCooldown = Mathf.Infinity;

    [SerializeField] private Transform playerClose;
    #endregion

    #region Basic_Method

    private void Update()
    {
        TimerCooldown += Time.deltaTime;

        ConditionAttack();
    }

    #endregion

    #region Custom_Method

    //Спавн ворогів по закінченню часу
    private void Attack()
    {
        TimerCooldown = 0;

        menoses[FindMenos()].transform.position = spawnPoint.position;
        menoses[FindMenos()].GetComponent<Patrool>().ActivateProjectile();
    }

    //пошук ворогів
    private int FindMenos()
    {
        for (int i = 0; i < menoses.Length; i++)
        {
            if (!menoses[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    //Умова(якщо ворог поряд)
    private void ConditionAttack()
    {
        if (TimerCooldown >= attackCoolDown && (transform.position.x - playerClose.position.x) < 14 && (transform.position.x - playerClose.position.x) > -5)
        {
            Attack();
        }
    }

    #endregion

}