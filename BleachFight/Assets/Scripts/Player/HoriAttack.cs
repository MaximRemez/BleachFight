using UnityEngine;

public class HoriAttack : MonoBehaviour
{
    #region Variables

    private BoxCollider2D boxCollider;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    //Наносити урону лише ворогам(для виключення помилок)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(0.5f);
        }
    }

    #endregion

    #region Custom_Method

    //активація зони урону
    public void SetDirection()
    {
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    //деактивація зони урону
    public void Diactivate()
    {
        gameObject.SetActive(false);
        boxCollider.enabled = false;
    }
    #endregion

}
