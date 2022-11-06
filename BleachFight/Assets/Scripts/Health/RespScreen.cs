using UnityEngine;

public class RespScreen : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject RespawnScreen;
    [SerializeField] private GameObject RespawnDark;

    private float StartTimer;
    private float TimeActivate= 2;

    //затримка після смерті
    private void Update()
    {
        if(health.CurrentHealth == 0)
        {
            StartTimer += Time.deltaTime;
        }

        if(StartTimer >= TimeActivate)
        {
            RespawnScreen.SetActive(true);
            RespawnDark.SetActive(true);
        }
    }

    //при натисканні на паузу
    public void ClickPause()
    {
        if (!RespawnScreen.activeInHierarchy)
        {
            RespawnScreen.SetActive(true);
            RespawnDark.SetActive(true);
        }
        else
        {
            RespawnScreen.SetActive(false);
            RespawnDark.SetActive(false);
        }
    }

}
