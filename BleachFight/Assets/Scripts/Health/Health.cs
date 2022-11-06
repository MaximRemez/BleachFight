using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Variables
    [Header("Health")]

    [SerializeField] private float startingHealth = 5;
    [SerializeField] private AudioClip HurtSound;
    [SerializeField] private AudioClip DieSound;
    public float CurrentHealth { get; private set; }

    private Animator anim;
    private bool dead;

    [Header("IFrames")]

    [SerializeField] private float iFramesDuration = 1;
    [SerializeField] private int numberFlashes = 3;
    private SpriteRenderer spriteRenderer;

    [Header("Components")]

    [SerializeField] private Behaviour[] components;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(10, 11, false);
        CurrentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #endregion

    #region Custom_Method

    //Отримання урону
    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);
      
        if (CurrentHealth > 0)// якщо живий
        {
            StartCoroutine(Invunerabillity());
            AudioManager.instance.PlaySound(HurtSound);
            anim.SetTrigger("hurt");
        }
        else//якщо помер
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                AudioManager.instance.PlaySound(DieSound);

                foreach (Behaviour component in components)// видалення компонентів для запобігання помилок
                {
                    component.enabled = false;
                }

                dead = true;
            }
         
        }
    }

    //Лічильник неуязвимості
    private IEnumerator Invunerabillity()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberFlashes; i++)//ефект миготіння
        {
            spriteRenderer.color = new Color(1, 0, 1, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberFlashes * 2.00f));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberFlashes * 2.00f));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
