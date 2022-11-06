using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class DoorScript : MonoBehaviour
{ 
    #region Variables

    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private PlayableDirector playable;

    public bool WinActivateLogic = false;
    public bool Played = false;
    #endregion

    #region Basic_Method

    //Активується при покиданні ігроком об'єкта( лише вправо )
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !Played)
        {
            if(collision.transform.position.x > transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
                playable.Play();
                Played = true;
                StartCoroutine(StartWinLogic());
            }
        }
    }

    #endregion

    #region Custom_Method

    //додається логіка перемоги(в кімнаті боса)
    private IEnumerator StartWinLogic()
    {
        yield return new WaitForSeconds(1);
        WinActivateLogic = true;
    }

    #endregion

}