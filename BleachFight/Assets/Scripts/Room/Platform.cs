using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Variables

    private float speed = 3.004f;
    private float StopTimer;
    private float StopDuration = 1.5f;

    [SerializeField] Light MapLight;
    [SerializeField] Light PlatformLight;

    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private ScoreManager score;
 
    private bool movingRight = true;
    private bool start = false;
    private bool IsPlayerOn = false;
    private bool IsMoved = false;

    [SerializeField] private GameObject NeedText;
    [SerializeField] private Transform Player;

    #endregion

    #region Basic_Method

    //коли ігрок заходить на платформу
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            IsPlayerOn = true;
        }
    }

    //Коли ігрок покидає платформу
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            IsPlayerOn = false;
            NeedText.SetActive(false);
        }
    }

    private void Update()
    {
        if (score.GetComponent<ScoreManager>().score >= 5)//активується після вбивства 5 ворогів
        {
            if (IsMoved)
            {
                StopTimer += Time.deltaTime;
            }
            else if (IsPlayerOn)
            {
                StopTimer += Time.deltaTime;
            }

            PlatformLogic();
        }
        else if (score.GetComponent<ScoreManager>().score < 5 && IsPlayerOn)
        {
            NeedText.SetActive(true);
        }

        MoveDiapason();
    }

    #endregion

    #region Custom_Method

    //логіка активації
    private void PlatformLogic()
    {
        if (StopTimer > StopDuration && start)
        {
            IsMoved = true;
            Move();
        }
    }

    // Діапазон руху платформи
    private void MoveDiapason()
    {
        if (transform.position.x >= point2.position.x)
        {
            movingRight = false;
            StartCoroutine(Duration());
        }
        else if (transform.position.x <= point1.position.x)
        {
            movingRight = true;
            StartCoroutine(Duration());
        }
    }

    private void Move()
    {
        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);

            if(point1.position.x - 1.5f <= Player.position.x && Player.position.x <= point2.position.x + 1f)// коли гравець поблизу платформи
            {
                if (MapLight.intensity >= 0)//зміна освітлення
                {
                    MapLight.intensity -= 0.003f;
                }
                if (PlatformLight.intensity <= 0.35f)
                {
                    PlatformLight.intensity += 0.002f;
                }
            }

        }
        if (!movingRight)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);

            if (point1.position.x - 1.5f <= Player.position.x && Player.position.x <= point2.position.x + 1f)// коли гравець поблизу платформи
            {
                if (MapLight.intensity <= 0.9f)//зміна освітлення
                {
                    MapLight.intensity += 0.003f;
                }
                if (PlatformLight.intensity >= 0)
                {
                    PlatformLight.intensity -= 0.001f;
                }
            }

        }
    }

    //затримка при змінні напрямку
    private IEnumerator Duration()
    {
        start = false;
        yield return new WaitForSeconds(1.5f);
        start = true;
    }

    #endregion

}
