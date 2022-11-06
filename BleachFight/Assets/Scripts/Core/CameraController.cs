using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables_Room_Camera

    [SerializeField] private float speed = 0.5f;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance = 3;
    [SerializeField] private float cameraSpeed = 0.7f;
    private float lookAhead;

    [SerializeField] private DoorScript door;
    private bool Nulled = false;
    private bool SlowChange = false;

    #endregion

    #region Basic_Method

    private void Update()
    {
        if (SlowChange)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z),
        ref velocity, speed);
        }

        if (!door.Played || Nulled)//зміна камери
        {
            PlayerCamera();

        }
        else if (door.Played && !Nulled)
        {
            RoomCamera();

            StartCoroutine(ChangeMod());
        }

    }
        
    #endregion

    #region Custom_Method

    // виставлення камери по центру кімнати
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x + 1.48f;
    }

    //переміщення камери у кімнату
    private void RoomCamera()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z),
           ref velocity, speed);
    }

    //Слежка камери за ігроком
    private void PlayerCamera()
    {
        transform.position = new Vector3(player.position.x + lookAhead,transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x)/3.00f, cameraSpeed * Time.deltaTime);
    }

    //плавна зміна камери
    private IEnumerator ChangeMod()
    {
        yield return new WaitForSeconds(2.7f);
        SlowChange = true;
        yield return new WaitForSeconds(0.4f);
        SlowChange = false;
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (1.5f * player.localScale.x) / 3.00f, cameraSpeed * Time.deltaTime);
        yield return new WaitForSeconds(0.7f);

        Nulled = true;
    }

    #endregion
}
