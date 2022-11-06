using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private Health BossHP;
    [SerializeField] private Health PlayerHP;
    [SerializeField] private Animator BossAnim;
    [SerializeField] private Animator PlayerAnim;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private DoorScript door;

    private bool played = false;

    private void Update()
    {
        if (door.WinActivateLogic)
        {
           
            if (played == false)
            {
                if (PlayerHP.GetComponent<Health>().CurrentHealth == 0)//при програші
                {
                    BossAnim.SetTrigger("kill");
                    played = true;
                }
                if (BossHP.GetComponent<Health>().CurrentHealth == 0)// при перемозі
                {
                    PlayerAnim.SetTrigger("win");
                    played = true;
                    foreach (Behaviour component in components)
                    {
                        component.enabled = false;
                    }
                }
            }
        }
    }
}
