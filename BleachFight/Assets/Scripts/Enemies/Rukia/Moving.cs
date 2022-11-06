using UnityEngine;

public class Moving : MonoBehaviour
{
    #region Variables

    [Header("Patrool")]

    [SerializeField] private Transform LeftEdge;
    [SerializeField] private Transform RightEdge;

    [Header("Rukia")]

    [SerializeField] private Transform Enemy;
    [SerializeField] private Transform Player;

    [Header("Move Parameters")]

    [SerializeField] private float speed = 4.5f;
    private Vector3 initialScale;
    private bool movingLeft = true;

    [Header("Enemy Animator")]

    [SerializeField] private Animator anim;
    [SerializeField] private float idleTime = 1f;
    private float timerIdle;
    private float SpawnTimer;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        initialScale = Enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        SpawnTimer += Time.deltaTime;

        PatroolLogic();
    }

    #endregion

    #region Custom_Method

    //Рух в напрямку
    private void MoveInDirection(int _direction)
    {
        timerIdle = 0;
        anim.SetBool("moving", true);

        Enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction, initialScale.y, initialScale.z);
        Enemy.position = new Vector3(Enemy.position.x + Time.deltaTime * _direction * speed, Enemy.position.y, Enemy.position.z);
    }

    //Зміна напрямку
    private void ChangeDirection()
    {
        anim.SetBool("moving", false);
        timerIdle += Time.deltaTime;

        if (timerIdle > idleTime)
        {
            movingLeft = !movingLeft;
        }

    }

    private void PatroolLogic()
    {
        if(SpawnTimer > 0.8f)//затримка при спавні
        {
            if (movingLeft)//рух вліво
            {
                if (Enemy.position.x >= LeftEdge.position.x && Enemy.position.x >= Player.position.x)
                {
                    MoveInDirection(-1);
                }
                else
                {
                    ChangeDirection();
                }
            }
            else//Рух вправо
            {
                if (Enemy.position.x <= RightEdge.position.x && Enemy.position.x <= Player.position.x)
                {
                    MoveInDirection(1);
                }
                else
                {
                    ChangeDirection();
                }
            }
        }
    }

    #endregion

    public void ActivateProjectile()
    {
        gameObject.SetActive(true);
    }

}
