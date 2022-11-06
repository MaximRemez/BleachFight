using UnityEngine;
using UnityEngine.Playables;

public class TimeLine : MonoBehaviour
{
    private bool fix = false;
    public Animator playerAnim;
    public RuntimeAnimatorController playerContr;
    public PlayableDirector director;

    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;

    //Забирання контролю в користувача
    private void OnEnable()
    {
        playerContr = playerAnim.runtimeAnimatorController;
        playerAnim.runtimeAnimatorController = null;
    }

    private void Update()
    {
        if(director.state != PlayState.Playing && !fix)
        {
            fix = true;
            playerAnim.runtimeAnimatorController = playerContr;//повертання контролю
        }
    }
}
