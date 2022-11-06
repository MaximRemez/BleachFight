using UnityEngine;

public class Patrool : MonoBehaviour
{
    #region Variables

    [Header("Patrool")]

    [SerializeField] private Transform LeftEdge;
    [SerializeField] private Transform RightEdge;

    [Header("Enemy")]

    [SerializeField] private Transform Enemy;

    [Header("Move Parameters")]

    [SerializeField] private float speed = 3;
    private Vector3 initialScale;
    private bool movingLeft;
    private float spawnTimer= 0;

    [Header("Enemy Animator")]

    [SerializeField] private Animator anim;
    [SerializeField] private float idleTime;
    private float timerIdle;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        initialScale = Enemy.localScale;

        System.Random random = new System.Random();//спавн в рандомному напрямку
        int test = random.Next(0,100);
        if (test > 50)
        {
            movingLeft = false;
        }
        else
        {
            movingLeft = true;
        }
    
    }

    //деактивація руху при смерті
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        PatroolLogic();
    }

    #endregion

    #region Custom_Method

    //початок руху
    private void MoveInDirection(int _direction)
    {
        timerIdle = 0;
        anim.SetBool("moving", true);

        Enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction, initialScale.y, initialScale.z);
        Enemy.position = new Vector3(Enemy.position.x + Time.deltaTime * _direction * speed, Enemy.position.y, Enemy.position.z);
    }

    //зміна руху
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
        if (spawnTimer > 0.7f)//затримка при старті
        {

            if (movingLeft)//рух вліво до упора
            {
                if (Enemy.position.x >= LeftEdge.position.x)
                {
                    MoveInDirection(-1);
                }
                else
                {
                    ChangeDirection();
                }
            }
            else//рух вправо до упора
            {
                if (Enemy.position.x <= RightEdge.position.x)
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
